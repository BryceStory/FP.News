using Dapper;
using FiiiPay.Enterprise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Data
{
    public class NewsDAC : BaseDataAccess
    {

        #region news start
        public News GetById(int id)
        {
            string sql = @"SELECT * FROM [dbo].[News] WHERE ID=@Id";

            using (var con = ReadConnection())
            {
                return con.QueryFirstOrDefault<News>(sql, new { Id = id });
            }
        }

        public List<News> GetPagedList(string title, byte? status, Guid accountId, ref Tuple<string, string, int, int, int, int> pager)
        {
            string sql = @"SELECT * FROM  [dbo].[News]";
            string countSql = "SELECT COUNT(1) FROM [dbo].[News]";
            string conditionSql = " WHERE 1=1  ";

            if (!string.IsNullOrWhiteSpace(title))
            {
                conditionSql += "  AND (CNTitle LIKE @Title OR ENTitle like @Title )";
            }

            string sql_statement = sql + conditionSql + $" ORDER BY CreateTime desc";
            sql_statement += $" OFFSET {(pager.Item3 - 1) * pager.Item4} ROW FETCH NEXT {pager.Item4} ROWS ONLY";

            using (var con = ReadConnection())
            {
                var sqlParam = new { Title = $"%{title}%", Status = status, AccountsId = accountId };
                var totalCount = con.ExecuteScalar<int>(countSql + conditionSql, sqlParam);
                int totalPage = totalCount == 0 ? 0 : ((int)Math.Ceiling((double)totalCount / (double)pager.Item4));
                pager = Tuple.Create(pager.Item1, pager.Item2, pager.Item3, pager.Item4, totalCount, totalPage);

                var result = con.Query<News>(sql_statement, sqlParam);
                return result.ToList();
            }
        }



        public bool Create(News model)
        {
            string sql = @"INSERT INTO [dbo].[News] ([CNTitle] ,[ENTitle] ,[CNPageView] ,[CNContent] ,[ENContent] ,[CNSource] ,[ENSource] ,[CNKeyword1] ,[ENKeyword1] ,
                          [CNCoverPictureUrl] ,[ENCoverPictureUrl] ,[Status] ,[CreateTime] ,[ModifyTime] ,[AccountsId] ,[ENPageView] ,[CNKeyword2] ,[ENKeyword2],[NewsSetionId],[CNPublisher],[ENPublisher],[CNIntro],[ENIntro])
                          VALUES (@CNTitle ,@ENTitle,@CNPageView ,@CNContent ,@ENContent,@CNSource,@ENSource,
                          @CNKeyword1,@ENKeyword1,@CNCoverPictureUrl,@ENCoverPictureUrl,@Status,@CreateTime,
                          @ModifyTime,@AccountsId,@ENPageView,@CNKeyword2,@ENKeyword2,@NewsSetionId,@CNPublisher,@ENPublisher,@CNIntro,@ENIntro)";

            using (var con = WriteConnection())
            {
                return con.Execute(sql, model) > 0;
            }
        }



        public bool UpdateCNVersion(News model)
        {
            string sql = @"UPDATE [dbo].[News] SET [CNTitle] = @CNTitle,[CNContent] = @CNContent,[CNSource] =@CNSource,[CNKeyword1] = @CNKeyword1,[CNCoverPictureUrl] = @CNCoverPictureUrl,                         
                        [Status] = @Status,[ModifyTime] = @ModifyTime,[CNKeyword2] = @CNKeyword2 ,[NewsSetionId]=@NewsSetionId,[CNPageView]=@CNPageView ,[CNPublisher]=@CNPublisher, [CNIntro]=@CNIntro WHERE  Id=@Id";

            using (var con = WriteConnection())
            {
                return con.Execute(sql, new
                {
                    CNPublisher = model.CNPublisher,
                    CNTitle = model.CNTitle,
                    CNContent = model.CNContent,
                    CNSource = model.CNSource,
                    CNKeyword1 = model.CNKeyword1,
                    CNKeyword2 = model.CNKeyword2,
                    CNCoverPictureUrl = model.CNCoverPictureUrl,
                    Status = model.Status,
                    Id = model.Id,
                    NewsSetionId = model.NewsSetionId,
                    CNPageView = model.CNPageView,
                    CNIntro = model.CNIntro,
                    ModifyTime = DateTime.UtcNow
                }) > 0;
            }
        }

        public bool UpdateENVersion(News model)
        {
            string sql = @"UPDATE [dbo].[News] SET [ENTitle] = @ENTitle,[ENContent] = @ENContent,[ENSource] =@ENSource,[ENKeyword1] = @ENKeyword1,[ENCoverPictureUrl] = @ENCoverPictureUrl,                         
                        [Status] = @Status,[ModifyTime] = @ModifyTime,[ENKeyword2] = @ENKeyword2 ,[NewsSetionId]=@NewsSetionId ,[ENPageView]=@ENPageView ,[ENPublisher]=@ENPublisher ,[ENIntro]=@ENIntro WHERE  Id=@Id";

            using (var con = WriteConnection())
            {
                return con.Execute(sql, new
                {
                    ENPublisher = model.ENPublisher,
                    ENTitle = model.ENTitle,
                    ENContent = model.ENContent,
                    ENSource = model.ENSource,
                    ENKeyword1 = model.ENKeyword1,
                    ENKeyword2 = model.ENKeyword2,
                    ENCoverPictureUrl = model.ENCoverPictureUrl,
                    Status = model.Status,
                    Id = model.Id,
                    NewsSetionId = model.NewsSetionId,
                    ENPageView = model.ENPageView,
                    ENIntro = model.ENIntro,
                    ModifyTime = DateTime.UtcNow
                }) > 0;
            }
        }

        public bool Unpublished(int id)
        {
            string sql = @"UPDATE [dbo].[News] SET Status=@Status,ModifyTime=GETDATE() WHERE ID=@Id";

            using (var con = WriteConnection())
            {
                return (con.Execute(sql, new
                {
                    Id = id,
                    Status = NewsStatus.Unpublished
                })) > 0;
            }
        }

        public bool Published(int id)
        {
            string sql = @"UPDATE [dbo].[News] SET Status=@Status,ModifyTime=GETDATE() WHERE ID=@Id";

            using (var con = WriteConnection())
            {
                return (con.Execute(sql, new
                {
                    Id = id,
                    Status = NewsStatus.Published
                })) > 0;
            }
        }

        public News CheckByCNTitle(News model)
        {
            string sql = @"select * from [dbo].[News] where CNTitle=@CNTitle and Id!=@Id ";

            using (var con = ReadConnection())
            {
                return con.QueryFirstOrDefault<News>(sql, new { CNTitle = model.CNTitle, Id = model.Id });
            }
        }

        public News CheckByENTitle(News model)
        {
            string sql = @"select * from [dbo].[News] where ENTitle=@ENTitle and Id!=@Id ";

            using (var con = ReadConnection())
            {
                return con.QueryFirstOrDefault<News>(sql, new { ENTitle = model.ENTitle, Id = model.Id });
            }
        }

        public bool Delete(int id)
        {
            string sql = @"DELETE FROM [dbo].[News] WHERE ID=@Id";

            using (var con = WriteConnection())
            {
                return (con.Execute(sql, new
                {
                    Id = id
                })) > 0;
            }
        }

        public IEnumerable<News> GetNewsList(int? newsSetionId, int pageSize, int pageIndex)
        {
            string sql = @"SELECT * FROM [dbo].[News] WHERE Status=@Status  ";

            if (newsSetionId != null)
            {
                sql += @"AND ( NewsSetionId=@NewsSetionId )";
            }

            sql += $" ORDER BY [Createtime] DESC OFFSET {pageSize * pageIndex} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            using (var con = ReadConnection())
            {
                return con.Query<News>(sql, new
                {
                    NewsSetionId = newsSetionId,
                    Status = (byte)NewsStatus.Published
                });
            }
        }

        public IEnumerable<NewsSetion> GetAllSectionList(int pageSize, int pageIndex)
        {
            string sql = @"SELECT * FROM [dbo].[NewsSetion]   ";
            sql += $" ORDER BY  [Sort] asc, [Createtime] DESC OFFSET {pageSize * pageIndex} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            using (var con = ReadConnection())
            {
                return con.Query<NewsSetion>(sql, new { });
            }
        }


        public async Task<int> GetCNPageViewByIdAsync(int newsId)
        {
            string sql = @"select CNPageView FROM [dbo].[News] where Id=@Id";

            using (var con = await ReadConnectionAsync())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = newsId });
            }
        }

        public async Task<int> GetENPageViewByIdAsync(int newsId)
        {
            string sql = @"select ENPageView FROM [dbo].[News] where Id=@Id";

            using (var con = await ReadConnectionAsync())
            {
                return await con.ExecuteScalarAsync<int>(sql, new { Id = newsId });
            }
        }

        public async Task<bool> UpdateCNPageViewByIdAsync(int newsId, int pageView)
        {
            string sql = @"UPDATE [dbo].[News] SET  [CNPageView]=@CNPageView WHERE Id=@Id";

            using (var con = await WriteConnectionAsync())
            {
                return await con.ExecuteAsync(sql, new
                {
                    CNPageView = pageView,
                    Id = newsId
                }) > 0;
            }
        }

        public async Task<bool> UpdateENPageViewByIdAsync(int newsId, int pageView)
        {
            string sql = @"UPDATE [dbo].[News] SET  [ENPageView]=@ENPageView WHERE Id=@Id";

            using (var con = await WriteConnectionAsync())
            {
                return await con.ExecuteAsync(sql, new
                {
                    ENPageView = pageView,
                    Id = newsId
                }) > 0;
            }
        }



        #endregion 

        #region newssection 
        public List<NewsSetion> GetSectionRecordList(string title, ref Tuple<string, string, int, int, int, int> pager)
        {
            string sql = @"SELECT * FROM  [dbo].[NewsSetion]";
            string countSql = "SELECT COUNT(1) FROM [dbo].[NewsSetion]";
            string conditionSql = " WHERE 1=1  ";

            if (!string.IsNullOrWhiteSpace(title))
            {
                conditionSql += "  AND (CNName LIKE @Title OR ENName like @Title )";
            }

            string sql_statement = sql + conditionSql + $" ORDER BY sort asc, CreateTime DESC";
            sql_statement += $" OFFSET {(pager.Item3 - 1) * pager.Item4} ROW FETCH NEXT {pager.Item4} ROWS ONLY";

            using (var con = ReadConnection())
            {
                var sqlParam = new { Title = $"%{title}%" };
                var totalCount = con.ExecuteScalar<int>(countSql + conditionSql, sqlParam);
                int totalPage = totalCount == 0 ? 0 : ((int)Math.Ceiling((double)totalCount / (double)pager.Item4));
                pager = Tuple.Create(pager.Item1, pager.Item2, pager.Item3, pager.Item4, totalCount, totalPage);

                var result = con.Query<NewsSetion>(sql_statement, sqlParam);
                return result.ToList();
            }
        }

        public NewsSetion GetNewsSectionById(int id)
        {
            string sql = @"SELECT * FROM [dbo].[NewsSetion] WHERE ID=@Id";

            using (var con = ReadConnection())
            {
                return con.QueryFirstOrDefault<NewsSetion>(sql, new { Id = id });
            }
        }

        public bool CreateNewsSection(NewsSetion model)
        {
            string sql = @"INSERT INTO [dbo].[NewsSetion] ([CNName],[ENName] ,[CreateTime] ,[Status] ,[AccountsId],[Sort])
                       VALUES (@CNName,@ENName,@CreateTime,@Status,@AccountsId,@Sort)";

            using (var con = WriteConnection())
            {
                return con.Execute(sql, model) > 0;
            }
        }

        public bool UpdateNewsSection(NewsSetion model)
        {
            string sql = @"UPDATE [dbo].[NewsSetion] SET [CNName] = @CNName,[ENName] = @ENName,[Sort] = @Sort WHERE Id=@Id";

            using (var con = WriteConnection())
            {
                return con.Execute(sql, new
                {
                    CNName = model.CNName,
                    ENName = model.ENName,
                    Sort = model.Sort,
                    Id = model.Id
                }) > 0;
            }
        }

        public bool DeleteNewsSectionById(int id)
        {
            string sql = @"DELETE FROM [dbo].[NewsSetion] WHERE ID=@Id";

            using (var con = WriteConnection())
            {
                return (con.Execute(sql, new
                {
                    Id = id
                })) > 0;
            }
        }

        public IEnumerable<NewsSetion> GetNewsSectionList()
        {
            string sql = @"SELECT * FROM [dbo].[NewsSetion]  ORDER BY  [Sort] asc, [Createtime] DESC ";

            using (var con = ReadConnection())
            {
                return con.Query<NewsSetion>(sql, new { });
            }
        }

        #endregion



    }
}
