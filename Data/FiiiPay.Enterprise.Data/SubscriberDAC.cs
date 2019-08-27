using Dapper;
using FiiiPay.Enterprise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Data
{
    public class SubscriberDAC : BaseDataAccess
    {
        public List<Subscriber> GetPagedList(string email, ref Tuple<string, string, int, int, int, int> pager)
        {
            string sql = @"SELECT * FROM  [dbo].[Subscriber]";
            string countSql = "SELECT COUNT(1) FROM [dbo].[Subscriber]";
            string conditionSql = " WHERE 1=1";

            if (!string.IsNullOrWhiteSpace(email))
            {
                conditionSql += "AND (Email LIKE @Email)";

            }

            string sql_statement = sql + conditionSql + $" ORDER BY CreateTime desc";
            sql_statement += $" OFFSET {(pager.Item3 - 1) * pager.Item4} ROW FETCH NEXT {pager.Item4} ROWS ONLY";

            using (var con = ReadConnection())
            {
                var sqlParam = new { Email = $"%{email}%" };
                var totalCount = con.ExecuteScalar<int>(countSql + conditionSql, sqlParam);
                int totalPage = totalCount == 0 ? 0 : ((int)Math.Ceiling((double)totalCount / (double)pager.Item4));
                pager = Tuple.Create(pager.Item1, pager.Item2, pager.Item3, pager.Item4, totalCount, totalPage);

                var result = con.Query<Subscriber>(sql_statement, sqlParam);
                return result.ToList();
            }
        }

        public bool Insert(string email)
        {
            string sql = @"INSERT INTO [dbo].[Subscriber] ([Email] ,[CreateTime]) VALUES (@Email,@CreateTime)";

            using (var con = WriteConnection())
            {
                return con.Execute(sql, new
                {
                    Email = email,
                    CreateTime = DateTime.UtcNow
                }) > 0;
            }
        }

    }
}
