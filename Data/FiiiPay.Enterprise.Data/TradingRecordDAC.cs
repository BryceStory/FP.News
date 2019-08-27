using Dapper;
using FiiiPay.Enterprise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Data
{
    public class TradingRecordDAC : BaseDataAccess
    {
        //pager:sortcolumn,orderby,page,size,totalCount,totalPage
        public List<Order> GetPagedList(string orderno, string username, byte? status, ref Tuple<string, string, int, int, int, int> pager)
        {
            string sql = "SELECT * FROM [dbo].[Orders]";
            string countSql = "SELECT COUNT(1) FROM [dbo].[Orders]";
            string conditionSql = " WHERE 1=1";
            if (!string.IsNullOrEmpty(orderno))
            {
                conditionSql += " AND OrderNo LIKE @OrderNo ";
            }
            else if (!string.IsNullOrEmpty(username))
            {
                conditionSql += " AND UserName LIKE @UserName";
            }
            else if (status.HasValue)
            {
                conditionSql += " AND Status = @Status";
            }

            string sql_statement = sql + conditionSql + $" ORDER BY Timestamp ASC";
            sql_statement += $" OFFSET {(pager.Item3 - 1) * pager.Item4} ROW FETCH NEXT {pager.Item4} ROWS ONLY";
            
            using (var con = ReadConnection())
            {
                var sqlParam = new { OrderNo = $"%{orderno}%", UserName = $"%{username}%", Status = status };
                var totalCount = con.ExecuteScalar<int>(countSql + conditionSql, sqlParam);
                int totalPage = totalCount == 0 ? 0 : ((int)Math.Ceiling((double)totalCount / (double)pager.Item4));
                pager = Tuple.Create(pager.Item1, pager.Item2, pager.Item3, pager.Item4, totalCount, totalPage);

                var result = con.Query<Order>(sql_statement, sqlParam);
                return result.ToList();
            }
        }
    }
}
