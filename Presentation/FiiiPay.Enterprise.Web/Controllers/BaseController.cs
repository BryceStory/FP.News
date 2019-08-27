using FiiiPay.Enterprise.Web.Models;
using FiiiPay.Framework.Cache;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace FiiiPay.Enterprise.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public string CurrentLanguage
        {
            get
            {
                return ViewBag.WebLanguage;
            }
        }

        public AccountInfo AccountInfo
        {
            get
            {
                var accountId = Request.Headers["useraccountId"].ToString();
                string redisKey = $"{Configs.PlateForm}:AccountInfo:{accountId}";
                if (!Guid.TryParse(accountId, out Guid gidAccountId))
                    return null;
                AccountInfo accountInfo = RedisHelper.Get<AccountInfo>(Configs.RedisIndex_Web, redisKey);
                if (accountInfo == null)
                {
                    var accountEntity = new Business.AccountComponent().GetAccountById(gidAccountId);
                    accountInfo = new AccountInfo
                    {
                        Id = accountEntity.Id,
                        Username = accountEntity.Username,
                        MerchantName = accountEntity.MerchantName,
                        // Currency = accountEntity.Currency,
                        Status = accountEntity.Status
                    };
                    RedisHelper.Set(Configs.RedisIndex_Web, redisKey, accountInfo, TimeSpan.FromDays(1));
                }
                return accountInfo;
            }
        }

        public void RefreshAccountInfo(AccountInfo accountInfo)
        {
            string redisKey = $"{Configs.PlateForm}:AccountInfo:{accountInfo.Id}";
            RedisHelper.Set(Configs.RedisIndex_Web, redisKey, accountInfo, TimeSpan.FromDays(1));
        }
    }
}