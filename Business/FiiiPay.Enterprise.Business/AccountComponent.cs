using FiiiPay.Enterprise.Data;
using FiiiPay.Enterprise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Business
{
    public class AccountComponent
    {
        public SaveResult CreateAccount(Account account, Guid userId, string userName)
        {
            if (CheckNameExt(account.Username))
            {
                return new SaveResult(false, "This username already exists!");
            }

            return new SaveResult(new AccountDAC().CreateAccount(account));
        }

        public SaveResult UpdateAccount(Account account, Guid userId, string userName)
        {
            Account oldAccount = GetAccountById(account.Id);
            oldAccount.RoleId = account.RoleId;
            oldAccount.Cellphone = account.Cellphone;
            oldAccount.Email = account.Email;
            oldAccount.ModifyBy = account.ModifyBy;
            oldAccount.ModifyTime = DateTime.Now;

            return new SaveResult(new AccountDAC().UpdateAccount(oldAccount));
        }

        public SaveResult DeleteAccount(Guid id)
        {
            var result = new AccountDAC().DeleteAccount(id);
            return new SaveResult(result);
        }

        public List<Account> GetAccountRecordList(string username, ref Tuple<string, string, int, int, int, int> pager)
        {
            var list = new AccountDAC().GetAccountRecordList(username, ref pager);

            return list.ToList();
        }
        public async Task<Account> GetAccountByUsernameAsync(string username)
        {
            return await new AccountDAC().SelectByUsernameAsync(username);
        }

        public Account GetAccountByUsername(string username)
        {
            return new AccountDAC().SelectByUsername(username);
        }

        public async Task<Account> GetAccountByEmailAsync(string email)
        {
            return await new AccountDAC().SelectByEmailAsync(email);
        }

        public Account GetAccountById(Guid accountId)
        {
            return new AccountDAC().SelectById(accountId);
        }

        public async Task<Account> GetAccountByIdAsync(Guid accountId)
        {
            return await new AccountDAC().SelectByIdAsync(accountId);
        }

        public async Task<bool> CheckEmailBind(Guid accountId, string email)
        {
            return await new AccountDAC().CheckEmailExistAsync(accountId, email);
        }

        public async Task<RequestResult> SetPasswordAndEmailAsync(Guid accountId, string password, string email, byte status)
        {
            var accountDAC = new AccountDAC();
            var existEmailAccount = await accountDAC.CheckEmailExistAsync(accountId, email);
            if (existEmailAccount)
                return new RequestResult(false, "AccountFirstSetting", "EmailBindByOtherAccount");
            await accountDAC.SetPasswordAndEmailAsync(accountId, password, email, status);
            return new RequestResult(true);
        }
        public async Task<bool> ResetPasswordAsync(Guid accountId, string password)
        {
            await new AccountDAC().SetPasswordAsync(accountId, password);
            return true;
        }
        public async Task<bool> ResetEmailAsync(Guid accountId, string email)
        {
            await new AccountDAC().SetEmailAsync(accountId, email);
            return true;
        }


        public bool CheckNameExt(string userName)
        {
            Account account = GetAccountByUsername(userName);
            return account != null;
        }
    }
}
