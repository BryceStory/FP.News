using Dapper;
using FiiiPay.Enterprise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Data
{
    public class AccountRoleDAC : BaseDataAccess
    {
        public bool CreateAccountRole(AccountRole model)
        {
            string sql = @"INSERT INTO [dbo].[AccountRole] ([CreateTime],[CreateBy],[ModifyTime],[ModifyBy],[Name],[Description])
                           VALUES (@CreateTime ,@CreateBy,@ModifyTime,@ModifyBy,@Name,@Description)";

            using (var con = WriteConnection())
            {
                return con.Execute(sql, model) > 0;
            }
        }

        public bool UpdateAccountRole(AccountRole model)
        {
            string sql = @"UPDATE [dbo].[AccountRole] SET [ModifyTime] = @ModifyTime,[ModifyBy] = @ModifyBy,[Name] = @Name,[Description] = @Description WHERE Id=@Id";

            using (var con = WriteConnection())
            {
                return con.Execute(sql, model) > 0;
            }
        }

        public bool DeleteAccountRole(int id)
        {
            string sql = @"DELETE FROM [dbo].[AccountRole] WHERE Id=@Id";

            using (var con = WriteConnection())
            {
                return (con.Execute(sql, new { Id = id })) > 0;
            }
        }

        public IEnumerable<AccountRole> GetRoleRecordList(string rolename, ref Tuple<string, string, int, int, int, int> pager)
        {
            string sql = "SELECT * FROM dbo.AccountRole WHERE 1=1 ";
            if (!string.IsNullOrWhiteSpace(rolename))
                sql += " AND [Name] like @Name";
            sql += $" ORDER BY CreateTime desc OFFSET {(pager.Item3 - 1) * pager.Item4} ROW FETCH NEXT {pager.Item4} ROWS ONLY";

            using (var con = ReadConnection())
            {
                return con.Query<AccountRole>(sql, new { Name = $"%{rolename}%" });
            }
        }

        public AccountRole GetAccountRoleById(int id)
        {
            string sql = @"SELECT * FROM dbo.AccountRole where Id=@Id";

            using (var con = ReadConnection())
            {
                return con.QueryFirstOrDefault<AccountRole>(sql, new { Id = id });
            }
        }

        public AccountRole GetAccountRoleByName(string name)
        {
            string sql = @"SELECT * FROM dbo.AccountRole where Name=@Name";

            using (var con = ReadConnection())
            {
                return con.QueryFirstOrDefault<AccountRole>(sql, new { Name = name.Trim() });
            }
        }
    }
}
