using FiiiPay.Enterprise.Entities;
using FiiiPay.Enterprise.Web.Models;
using FiiiPay.Enterprise.Web.Utils;
using FiiiPay.Framework.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FiiiPay.Enterprise.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class AccessAttribute : AuthorizeAttribute
    {
        public AccessAttribute()
        {
        }
        private bool onlyFirstLoginAction = false;
        private AccountInfo accountInfo;
        private string languageName;
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentException();
            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
                throw new InvalidOperationException();

            languageName = filterContext.RouteData.Values["lang"].ToString().ToLower();
            List<string> langList = new List<string> { "cn", "en" };
            if (!langList.Contains(languageName))
                languageName = CultureHelper.GetLanguage();
            //将语言默认为中文版
            //languageName = "cn";  //911

            filterContext.Controller.ViewBag.WebLanguage = languageName;

            bool flag = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), false);
            if (flag)
                return;
            bool isChildAction = filterContext.ActionDescriptor.IsDefined(typeof(ChildActionOnlyAttribute), false);
            if (isChildAction)
                return;

            onlyFirstLoginAction = filterContext.ActionDescriptor.IsDefined(typeof(FirstLoginOnlyAttribute), false);
            if (!AuthorizeCore(filterContext.HttpContext))
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.AddHeader("sessionstatus", "timeout");
                }
                this.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                if (!filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Controller.ViewBag.MerchantName = accountInfo?.Username;
                    //filterContext.Controller.ViewBag.WebLanguage = languageName;
                }
            }
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var logininfo = httpContext.Request.Cookies[Configs.CookieKey_LoginInfo];
            if (logininfo == null)
                return false;

            var cookieValue = Encrypts.GetDecryptString(logininfo.Value.ToString());
            var cookieValues = cookieValue.Split(new char[] { '_' });
            if (cookieValues == null || cookieValues.Length != 3)
                return false;
            try
            {
                var accountId = Guid.Parse(cookieValues[0]);
                var randomString = cookieValues[1].ToString();
                var ticks = long.Parse(cookieValues[2]);
                DateTime dtNow = DateTime.UtcNow;
                DateTime dtToken = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(ticks);
                var totalMinutes = (dtNow - dtToken).TotalMinutes;
                if ((dtNow - dtToken).TotalMinutes > 35)//长时间没访问
                    return false;

                string redisKey = $"{Configs.PlateForm}:AccountInfo:{accountId}";

                accountInfo = RedisHelper.Get<AccountInfo>(Configs.RedisIndex_Web, redisKey);

                if (accountInfo == null)
                    return false;

                if (accountInfo.Token != randomString)
                    return false;

                if (accountInfo.Status == (byte)AccountStatus.Disabled)
                    return false;

                if ((dtNow - dtToken).TotalMinutes > 5)//最少5分钟更新一次cookie和Redis
                {
                    httpContext.Response.Cookies.Add(httpContext.Request.Cookies[Configs.CookieKey_LoginInfo]);
                    httpContext.Response.Cookies[Configs.CookieKey_LoginInfo].Value = Encrypts.GetEncryptString(accountId.ToString() + "_" + randomString);
                    httpContext.Response.Cookies[Configs.CookieKey_LoginInfo].HttpOnly = true;

                    RedisHelper.KeyExpire(Configs.RedisIndex_Web, redisKey, TimeSpan.FromDays(1));
                }

                httpContext.Request.Headers.Add("useraccountId", accountId.ToString());

                return AuthorizeRedirect(httpContext, accountInfo.Status);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool AuthorizeRedirect(HttpContextBase httpContext, byte status)
        {
            if (onlyFirstLoginAction && status != (byte)AccountStatus.Registered)
            {
                httpContext.Response.RedirectToRoute("culturelogin", new { lang = languageName });
                return true;
            }
            if (!onlyFirstLoginAction && status == (byte)AccountStatus.Registered)
            {
                httpContext.Response.RedirectToRoute("firstsetting", new { lang = languageName });
                return true;
            }
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectResult($"/{languageName}/login", false);
        }
    }
}