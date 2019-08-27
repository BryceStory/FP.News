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
    public class BannerDAC : BaseDataAccess
    {
        public int InsertBanner(Banner model)
        {
            string sql = @"INSERT INTO [dbo].[Banner] ([AccountId] ,[Title], [LinkUrl] ,[Version] ,[Status] ,[Type],
                         [CreateTime] ,[ModifyTime] ,[Sort])
                          VALUES (@AccountId ,@Title,@LinkUrl ,@Version ,@Status,@Type,
                          @CreateTime,@ModifyTime,@Sort);SELECT SCOPE_IDENTITY()";

            using (var con = WriteConnection())
            {
                return con.ExecuteScalar<int>(sql, model);
            }
        }

        public bool InsertBannerFiles(int bannerId, string[] bannerUrl)
        {
            string sql = @"INSERT INTO [dbo].[BannerFiles]([BannerId] ,[FileUrl] ,[CreateTime])
                         VALUES (@BannerId,@FileUrl,@CreateTime)";

            using (var con = WriteConnection())
            {
                var result = false;

                foreach (var item in bannerUrl)
                {
                    result = con.Execute(sql, new
                    {
                        BannerId = bannerId,
                        FileUrl = item,
                        CreateTime = DateTime.UtcNow
                    }) > 0;
                }
                return result;
            }
        }

        public bool UpdateBanner(Banner model)
        {
            string sql = @"UPDATE [dbo].[Banner] SET [AccountId] = @AccountId,[Title] = @Title,[LinkUrl] = @LinkUrl,[Version] = @Version,[Status] = @Status,[Type] = @Type,[CreateTime] = @CreateTime
                          ,[ModifyTime] = @ModifyTime,[Sort] = @Sort  WHERE Id=@Id";

            using (var con = WriteConnection())
            {
                return con.Execute(sql, model) > 0;
            }
        }

        public Banner CheckTitleExist(Banner banner)
        {
            string sql = @"SELECT * FROM [dbo].[Banner] WHERE Title=@Title AND Version=@Version AND Type=@Type and Id!=@Id";

            using (var con = ReadConnection())
            {
                return con.QueryFirstOrDefault<Banner>(sql, new { Title = banner.Title, Version = banner.Version, Type = banner.Type ,Id=banner.Id});
            }
        }

        public bool DeleteBannerById(int id)
        {
            string sql = @"DELETE FROM [dbo].[Banner] WHERE ID=@Id";

            using (var con = WriteConnection())
            {
                return (con.Execute(sql, new
                {
                    Id = id
                })) > 0;
            }
        }

        public bool DeleteBannerFileByBannerId(int bannerId)
        {
            string sql = @"DELETE FROM [dbo].[BannerFiles] WHERE BannerId=@BannerId";

            using (var con = WriteConnection())
            {
                return (con.Execute(sql, new
                {
                    BannerId = bannerId
                })) > 0;
            }
        }

        public List<Banner> GetHomePageRecordList(string title, int Version, ref Tuple<string, string, int, int, int, int> pager)
        {
            string sql = @"SELECT * FROM  [dbo].[Banner]";
            string countSql = "SELECT COUNT(1) FROM [dbo].[Banner]";
            string conditionSql = " WHERE 1=1 AND Type=@Type ";

            if (!string.IsNullOrWhiteSpace(title))
            {
                conditionSql += " AND (Title LIKE @Title )";
            }
            if (Version == (int)BannerVersion.CN || Version == (int)BannerVersion.EN)
            {
                conditionSql += " AND (Version=@Version )";
            }
            string sql_statement = sql + conditionSql + $" ORDER BY sort asc, CreateTime DESC";
            sql_statement += $" OFFSET {(pager.Item3 - 1) * pager.Item4} ROW FETCH NEXT {pager.Item4} ROWS ONLY";

            using (var con = ReadConnection())
            {
                var sqlParam = new { Title = $"%{title}%", Type = (int)BannerType.HomePageBanner, Version = Version };
                var totalCount = con.ExecuteScalar<int>(countSql + conditionSql, sqlParam);
                int totalPage = totalCount == 0 ? 0 : ((int)Math.Ceiling((double)totalCount / (double)pager.Item4));
                pager = Tuple.Create(pager.Item1, pager.Item2, pager.Item3, pager.Item4, totalCount, totalPage);

                var result = con.Query<Banner>(sql_statement, sqlParam);
                return result.ToList();
            }
        }
        public List<Banner> GetFiiiRecordList(string title, int Version, ref Tuple<string, string, int, int, int, int> pager)
        {
            string sql = @"SELECT * FROM  [dbo].[Banner]";
            string countSql = "SELECT COUNT(1) FROM [dbo].[Banner]";
            string conditionSql = " WHERE 1=1 AND Type=@Type ";

            if (!string.IsNullOrWhiteSpace(title))
            {
                conditionSql += " AND (Title LIKE @Title )";
            }
            if (Version == (int)BannerVersion.CN || Version == (int)BannerVersion.EN)
            {
                conditionSql += " AND (Version=@Version )";
            }
            string sql_statement = sql + conditionSql + $" ORDER BY sort asc, CreateTime DESC";
            sql_statement += $" OFFSET {(pager.Item3 - 1) * pager.Item4} ROW FETCH NEXT {pager.Item4} ROWS ONLY";

            using (var con = ReadConnection())
            {
                var sqlParam = new { Title = $"%{title}%", Type = (int)BannerType.FiiiEcologyBanner, Version = Version };
                var totalCount = con.ExecuteScalar<int>(countSql + conditionSql, sqlParam);
                int totalPage = totalCount == 0 ? 0 : ((int)Math.Ceiling((double)totalCount / (double)pager.Item4));
                pager = Tuple.Create(pager.Item1, pager.Item2, pager.Item3, pager.Item4, totalCount, totalPage);

                var result = con.Query<Banner>(sql_statement, sqlParam);
                return result.ToList();
            }
        }

        public List<Banner> GetNewsAdvRecordList(string title, int Version, ref Tuple<string, string, int, int, int, int> pager)
        {
            string sql = @"SELECT * FROM  [dbo].[Banner]";
            string countSql = "SELECT COUNT(1) FROM [dbo].[Banner]";
            string conditionSql = " WHERE 1=1 AND Type=@Type ";

            if (!string.IsNullOrWhiteSpace(title))
            {
                conditionSql += " AND (Title LIKE @Title )";
            }
            if (Version == (int)BannerVersion.CN || Version == (int)BannerVersion.EN)
            {
                conditionSql += " AND (Version=@Version )";
            }
            string sql_statement = sql + conditionSql + $" ORDER BY sort asc, CreateTime DESC";
            sql_statement += $" OFFSET {(pager.Item3 - 1) * pager.Item4} ROW FETCH NEXT {pager.Item4} ROWS ONLY";

            using (var con = ReadConnection())
            {
                var sqlParam = new { Title = $"%{title}%", Type = (int)BannerType.NewsBanner, Version = Version };
                var totalCount = con.ExecuteScalar<int>(countSql + conditionSql, sqlParam);
                int totalPage = totalCount == 0 ? 0 : ((int)Math.Ceiling((double)totalCount / (double)pager.Item4));
                pager = Tuple.Create(pager.Item1, pager.Item2, pager.Item3, pager.Item4, totalCount, totalPage);

                var result = con.Query<Banner>(sql_statement, sqlParam);
                return result.ToList();
            }
        }
        public Banner GetById(int id)
        {
            string sql = @"select * from [dbo].[Banner] where Id=@id";

            using (var con = ReadConnection())
            {
                return con.QueryFirstOrDefault<Banner>(sql, new { Id = id });
            }
        }
        public BannerOM GetCheckById(int id)
        {
            string sql = @"select a.Id, a.Title,a.LinkUrl,a.Version ,a.Status,a.Type,a.CreateTime,a.Sort,b.FileUrl from [dbo].[Banner] a
                           left join [dbo].[BannerFiles] b on a.Id=b.BannerId and Status=@Status where a.Id=@Id";

            using (var con = ReadConnection())
            {
                return con.QueryFirstOrDefault<BannerOM>(sql, new { Id = id, Status = (int)BannerStatus.Published });
            }
        }


        public IEnumerable<BannerListOM> GetBannerListByType(int pageSize, int pageIndex, int type, int version)
        {
            string sql = @" select a.Id, a.Title,a.LinkUrl,a.Version ,a.Status,a.Type,a.CreateTime,a.Sort,b.FileUrl from [dbo].[Banner] a
                           left join [dbo].[BannerFiles] b on a.Id=b.BannerId and Status=@Status where a.Type=@Type and a.Version=@Version ";
            sql += $" order by Sort asc, CreateTime desc  OFFSET {pageSize * pageIndex} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            using (var con = ReadConnection())
            {
                return con.Query<BannerListOM>(sql, new
                {
                    Type = type,
                    Status = (int)BannerStatus.Published,
                    Version = version
                });
            }
        }

        public BannerFiles GetBannerFileByBannerId(int bannerId)
        {
            string sql = @"SELECT * FROM [dbo].[BannerFiles] where BannerId=@BannerId  ";

            using (var con = ReadConnection())
            {
                return con.QueryFirstOrDefault<BannerFiles>(sql, new
                {
                    BannerId = bannerId
                });
            }
        }

        public bool Unpublished(int id)
        {
            string sql = @"UPDATE [dbo].[Banner] SET Status=@Status,ModifyTime=GETDATE() WHERE ID=@Id";

            using (var con = WriteConnection())
            {
                return (con.Execute(sql, new
                {
                    Id = id,
                    Status = BannerStatus.Unpublished
                })) > 0;
            }
        }
        public bool Published(int id)
        {
            string sql = @"UPDATE [dbo].[Banner] SET Status=@Status,ModifyTime=GETDATE() WHERE ID=@Id";

            using (var con = WriteConnection())
            {
                return (con.Execute(sql, new
                {
                    Id = id,
                    Status = BannerStatus.Published
                })) > 0;
            }
        }

    }
}
