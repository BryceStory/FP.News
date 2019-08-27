using Dapper;
using FiiiPay.Enterprise.DTO;
using FiiiPay.Enterprise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Data
{
    public class NewsFlashDAC : BaseDataAccess
    {
        public NewsFlash GetById(int id)
        {
            string sql = @"SELECT * FROM [dbo].[NewsFlash] WHERE ID=@Id";

            using (var con = ReadConnection())
            {
                return con.QueryFirstOrDefault<NewsFlash>(sql, new { Id = id });
            }
        }

        public List<NewsFlashFiles> GetFlashFilesById(int newsFlashId)
        {
            string sql = @" SELECT * FROM [dbo].[NewsFlashFiles] WHERE NewsFlashId=@NewsFlashId";

            using (var con = ReadConnection())
            {
                return con.Query<NewsFlashFiles>(sql, new { NewsFlashId = newsFlashId }).ToList();
            }
        }

        public List<NewsFlash> GetPagedList(string title, byte? status, ref Tuple<string, string, int, int, int, int> pager)
        {
            string sql = @"SELECT * FROM  [dbo].[NewsFlash]";
            string countSql = "SELECT COUNT(1) FROM [dbo].[NewsFlash]";
            string conditionSql = " WHERE 1=1";

            if (!string.IsNullOrWhiteSpace(title))
            {
                conditionSql += "AND (CNTitle LIKE @Title OR ENTitle like @Title )";

            }

            string sql_statement = sql + conditionSql + $" ORDER BY CreateTime DESC";
            sql_statement += $" OFFSET {(pager.Item3 - 1) * pager.Item4} ROW FETCH NEXT {pager.Item4} ROWS ONLY";

            using (var con = ReadConnection())
            {
                var sqlParam = new { Title = $"%{title}%", Status = status };
                var totalCount = con.ExecuteScalar<int>(countSql + conditionSql, sqlParam);
                int totalPage = totalCount == 0 ? 0 : ((int)Math.Ceiling((double)totalCount / (double)pager.Item4));
                pager = Tuple.Create(pager.Item1, pager.Item2, pager.Item3, pager.Item4, totalCount, totalPage);

                var result = con.Query<NewsFlash>(sql_statement, sqlParam);
                return result.ToList();
            }
        }

        public int CreateNewsFlash(NewsFlash model)
        {
            string sql = @"INSERT INTO [dbo].[NewsFlash] ([CNTitle] ,[ENTitle] ,[CNPageView] ,[CNContent] ,[ENContent] ,[CNSource] ,[ENSource],
                         [Status] ,[CreateTime] ,[ModifyTime] ,[AccountsId] ,[ENPageView],[CNGood],[CNBad],[ENGood],[ENBad])
                          VALUES (@CNTitle ,@ENTitle,@CNPageView ,@CNContent ,@ENContent,@CNSource,@ENSource,
                         @Status,@CreateTime,@ModifyTime,@AccountsId,@ENPageView ,@CNGood ,@CNBad ,@ENGood ,@ENBad );SELECT SCOPE_IDENTITY()";

            using (var con = WriteConnection())
            {
                return con.ExecuteScalar<int>(sql, model);
            }
        }

        public bool CreateNewsFlashFiles(int newsFlashId, string[] pictureUrls, int version)
        {
            string sql = @"INSERT INTO [dbo].[NewsFlashFiles] ([NewsFlashId] ,[CoverPictureUrl] ,[CreateTime] ,[Version]) VALUES (@NewsFlashId,@CoverPictureUrl,@CreateTime,@Version )";

            using (var con = WriteConnection())
            {
                var result = false;
                foreach (var item in pictureUrls)
                {
                    result = con.Execute(sql, new
                    {
                        NewsFlashId = newsFlashId,
                        CoverPictureUrl = item,
                        CreateTime = DateTime.UtcNow,
                        Version = version
                    }) > 0;
                }


                return result;
            }
        }
        public bool UpdateCNVersion(NewsFlash model)
        {
            string sql = @"UPDATE [dbo].[NewsFlash] SET [CNTitle] = @CNTitle,[CNContent] = @CNContent,[CNSource] =@CNSource,                         
                        [Status] = @Status,[ModifyTime] = @ModifyTime ,[CNGood]=@CNGood ,[CNBad]=@CNBad,[ENGood]=@CNGood ,[ENBad]=@CNBad  WHERE  Id=@Id";

            using (var con = WriteConnection())
            {
                return con.Execute(sql, new
                {
                    CNTitle = model.CNTitle,
                    CNContent = model.CNContent,
                    CNSource = model.CNSource,
                    Status = model.Status,
                    Id = model.Id,
                    ModifyTime = DateTime.UtcNow,
                    CNGood = model.CNGood,    //中英文版本 利空利好数同步 所以更新中文版时，英文版的数量也同步为中文版输入的数量 ，英文版反之
                    CNBad = model.CNBad,
                    ENGood = model.CNGood,
                    ENBad = model.CNBad
                }) > 0;
            }
        }

        public bool UpdateENVersion(NewsFlash model)
        {
            string sql = @"UPDATE [dbo].[NewsFlash] SET [ENTitle] = @ENTitle,[ENContent] = @ENContent,[ENSource] =@ENSource,                         
                        [Status] = @Status,[ModifyTime] = @ModifyTime ,[CNGood]=@ENGood ,[CNBad]=@ENBad,[ENGood]=@ENGood ,[ENBad]=@ENBad WHERE  Id=@Id";

            using (var con = WriteConnection())
            {
                return con.Execute(sql, new
                {
                    ENTitle = model.ENTitle,
                    ENContent = model.ENContent,
                    ENSource = model.ENSource,
                    Status = model.Status,
                    Id = model.Id,
                    ModifyTime = DateTime.UtcNow,
                    ENGood = model.ENGood,
                    ENBad = model.ENBad,
                    CNGood = model.ENGood,
                    CNBad = model.ENBad
                }) > 0;
            }
        }

        public NewsFlash CheckByCNTitle(NewsFlash model)
        {
            string sql = @"select * from [dbo].[NewsFlash] where CNTitle=@CNTitle and Id!=@Id ";

            using (var con = ReadConnection())
            {
                return con.QueryFirstOrDefault<NewsFlash>(sql, new { CNTitle = model.CNTitle , Id =model.Id});
            }
        }

        public NewsFlash CheckByENTitle(NewsFlash model)
        {
            string sql = @"select * from [dbo].[NewsFlash] where ENTitle=@ENTitle  and Id!=@Id  ";

            using (var con = ReadConnection())
            {
                return con.QueryFirstOrDefault<NewsFlash>(sql, new { ENTitle = model.ENTitle, Id = model.Id });
            }
        }



        public bool Unpublished(int id)
        {
            string sql = @"UPDATE [dbo].[NewsFlash] SET Status=@Status,ModifyTime=GETDATE() WHERE ID=@Id";

            using (var con = WriteConnection())
            {
                return (con.Execute(sql, new
                {
                    Id = id,
                    Status = NewsFlashStatus.Unpublished
                })) > 0;
            }
        }
        public bool Published(int id)
        {
            string sql = @"UPDATE [dbo].[NewsFlash] SET Status=@Status,ModifyTime=GETDATE() WHERE ID=@Id";

            using (var con = WriteConnection())
            {
                return (con.Execute(sql, new
                {
                    Id = id,
                    Status = NewsFlashStatus.Published
                })) > 0;
            }
        }
        public bool DeleteFlash(int id)
        {
            string sql = @"DELETE FROM [dbo].[NewsFlash] WHERE ID=@Id";

            using (var con = WriteConnection())
            {
                return (con.Execute(sql, new
                {
                    Id = id
                })) > 0;
            }
        }

        public bool DeleteFlashFilesById(int newsFlashId)
        {
            string sql = @"DELETE FROM [dbo].[NewsFlashFiles] WHERE NewsFlashId=@NewsFlashId";

            using (var con = WriteConnection())
            {
                return (con.Execute(sql, new
                {
                    NewsFlashId = newsFlashId
                })) > 0;
            }
        }

        public bool DeleteFlashFilesByIdAndVer(int newsFlashId, int version)
        {
            string sql = @"DELETE FROM [dbo].[NewsFlashFiles] WHERE NewsFlashId=@NewsFlashId and Version=@Version";

            using (var con = WriteConnection())
            {
                return (con.Execute(sql, new
                {
                    NewsFlashId = newsFlashId,
                    Version = version
                })) > 0;
            }
        }

        public IEnumerable<NewsFlash> GetNewsFlashList(int pageSize, int pageIndex)
        {
            string sql = @"SELECT * FROM [dbo].[NewsFlash] WHERE Status=@Status ";
            sql += $" ORDER BY [Createtime] desc OFFSET {pageSize * pageIndex} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            using (var con = ReadConnection())
            {
                return con.Query<NewsFlash>(sql, new
                {
                    Status = (byte)NewsFlashStatus.Published
                });
            }
        }

        public async Task<int> GetGoodByIdAsync(int newsFlashId)
        {
            string sql = @"select CNGood FROM [dbo].[NewsFlash] where Id=@Id";

            using (var con = await ReadConnectionAsync())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = newsFlashId });
            }
        }

        public async Task<int> GetBadByIdAsync(int newsFlashId)
        {
            string sql = @"select CNBad FROM [dbo].[NewsFlash] where Id=@Id";

            using (var con = await ReadConnectionAsync())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = newsFlashId });
            }
        }

        public async Task<bool> UpdateGoodByIdAsync(int newsFlashId, int good)
        {
            string sql = @"  UPDATE [dbo].[NewsFlash] SET CNGood=@CNGood ,ENGood=@ENGood WHERE Id=@Id";

            using (var con = await WriteConnectionAsync())
            {
                return await con.ExecuteAsync(sql, new
                {
                    CNGood = good,
                    ENGood = good,
                    Id = newsFlashId
                }) > 0;
            }
        }

        public async Task<bool> UpdateBadByIdAsync(int newsFlashId, int bad)
        {
            string sql = @"  UPDATE [dbo].[NewsFlash] SET CNBad=@CNBad ,ENBad=@ENBad WHERE Id=@Id";

            using (var con = await WriteConnectionAsync())
            {
                return await con.ExecuteAsync(sql, new
                {
                    CNBad = bad,
                    ENBad = bad,
                    Id = newsFlashId
                }) > 0;
            }
        }

    }
}
