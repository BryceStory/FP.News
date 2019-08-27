using FiiiPay.Enterprise.Data;
using FiiiPay.Enterprise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Business
{
    public class AccountRoleComponent
    {
        public List<AccountRole> GetRoleRecordList(string rolename, ref Tuple<string, string, int, int, int, int> pager)
        {
            var list = new AccountRoleDAC().GetRoleRecordList(rolename, ref pager);

            return list.ToList();
        }

        public AccountRole GetById(int id)
        {
            return new AccountRoleDAC().GetAccountRoleById(id);
        }

        public AccountRole GetAccountRoleByName(string name)
        {
            return new AccountRoleDAC().GetAccountRoleByName(name);
        }

        public SaveResult CreateRole(AccountRole role)
        {
            if (CheckNameExt(role.Name))
            {
                return new SaveResult(false, "This Name already exists!");
            }

            return new SaveResult(new AccountRoleDAC().CreateAccountRole(role));
        }

        public SaveResult UpdateRole(AccountRole role)
        {
            return new SaveResult(new AccountRoleDAC().UpdateAccountRole(role));
        }

        public SaveResult DeleteRole(int id)
        {
            var result = new AccountRoleDAC().DeleteAccountRole(id);
            return new SaveResult(result);
        }

        public bool CheckNameExt(string roleName)
        {
            AccountRole role = GetAccountRoleByName(roleName);
            return role != null;
        }
    }
}
