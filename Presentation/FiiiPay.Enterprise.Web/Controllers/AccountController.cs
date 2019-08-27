using FiiiPay.Enterprise.Business;
using FiiiPay.Enterprise.Entities;
using FiiiPay.Enterprise.Web.Models;
using FiiiPay.Enterprise.Web.Resources;
using FiiiPay.Enterprise.Web.Utils;
using FiiiPay.Framework.Cache;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FiiiPay.Enterprise.Web.Controllers
{
    [Access]
    public class AccountController : BaseController
    {
        private AccountComponent _component = new AccountComponent();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var account = await new AccountComponent().GetAccountByUsernameAsync(model.MerchantId);
            if (account == null)
            {
                ModelState.AddModelError("", AccountLogin.MerchantIdNotExist);
                return View(model);
            }
            try
            {
                SecurityVerify.Verify<PasswordVerification>(account.Id.ToString(), account.Password, model.Password);
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", AccountLogin.Loginfailed);
                return View(model);
            }

            if (account.Status == (byte)AccountStatus.Disabled)
            {
                ModelState.AddModelError("", AccountLogin.AccountDisabed);
                return View(model);
            }

            string accountId = account.Id.ToString();
            string randomString = Framework.RandomAlphaNumericGenerator.Generate(8);
            var userCookie = Request.Cookies[Configs.CookieKey_LoginInfo];
            if (userCookie == null)
            {
                var cookie = Response.Cookies[Configs.CookieKey_LoginInfo];
                cookie.Value = Encrypts.GetEncryptString(accountId + "_" + randomString);
                cookie.HttpOnly = true;
            }
            else
            {
                Response.Cookies.Add(Request.Cookies[Configs.CookieKey_LoginInfo]);
                Response.Cookies[Configs.CookieKey_LoginInfo].Value = Encrypts.GetEncryptString(accountId + "_" + randomString);
                Response.Cookies[Configs.CookieKey_LoginInfo].HttpOnly = true;
            }
            if (account.Status == (byte)AccountStatus.Registered)
            {
                returnUrl = $"/{CurrentLanguage}/firstsetting";
            }

            AccountInfo accountInfo = new AccountInfo
            {
                Id = account.Id,
                Username = account.Username,
                MerchantName = account.MerchantName,
                // Currency = account.Currency,
                Status = account.Status,
                Token = randomString
            };

            RefreshAccountInfo(accountInfo);

            return RedirectToLocal(returnUrl);
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadAccountData(AccountSearchModel model, GridPager pager)
        {
            var pagerTuple = Tuple.Create(pager.SortColumn, pager.OrderBy, pager.Page, pager.Size, 0, 0);
            var result = _component.GetAccountRecordList(model.Username, ref pagerTuple);
            pager.Count = pagerTuple.Item5;
            pager.TotalPage = pagerTuple.Item6;

            var models = result.ToGridJson(pager, item => new
            {
                Id = item.Id,
                Username = item.Username,
                MerchantName = item.MerchantName == null ? "" : item.MerchantName,
                Cellphone = item.Cellphone,
                Email = item.Email == null ? "" : item.Email,
                Status = item.Status,
                Operate = item.Id

            });

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditAccount(Guid id)
        {
            Account account = new Account();
            if (id != Guid.Empty)
            {
                 account = _component.GetAccountById(id);
            }

            return View(account);
        }

        [HttpPost]
        public ActionResult Save(Account oAccount)
        {
            if (oAccount.Id != Guid.Empty) //编辑
            {
                return Json(SaveEdit(oAccount).toJson());
            }
            else //新增
            {
                return Json(SaveCreate(oAccount).toJson());
            }
        }

        private SaveResult SaveCreate(Account oAccount)
        {
            oAccount.Id = Guid.NewGuid();
            oAccount.Status = (byte)AccountStatus.Active;
            oAccount.PIN = "";
            oAccount.CreateTime = DateTime.Now;
            oAccount.CreateBy = AccountInfo.Id;
            oAccount.RoleId = null;   //角色先暂定为空
            oAccount.Password = PasswordHasher.HashPassword(oAccount.Password);

            return _component.CreateAccount(oAccount, AccountInfo.Id, AccountInfo.Username);
        }

        private SaveResult SaveEdit(Account oAccount)
        {
            oAccount.ModifyBy = AccountInfo.Id;
            oAccount.ModifyTime = DateTime.Now;

            return _component.UpdateAccount(oAccount, AccountInfo.Id, AccountInfo.Username);
        }

        public ActionResult DeleteAccount(Guid id)
        {
            return Json(_component.DeleteAccount(id).toJson());
        }


        public ActionResult ResetPsw()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPsw(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var accountComponent = new AccountComponent();
            var account = await accountComponent.GetAccountByIdAsync(AccountInfo.Id);
            try
            {
                SecurityVerify.Verify<PasswordVerification>(account.Id.ToString(), account.Password, model.OldPassword);
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", GeneralResource.SaveFailed);
                return View(model);
            }
            await accountComponent.ResetPasswordAsync(account.Id, PasswordHasher.HashPassword(model.NewPassword));
            EmptyLoginInfo();
            ViewBag.PasswordHasSet = "1";
            ViewBag.PageName = MerchantIndex.Pagename;

            return View(model);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            EmptyLoginInfo();
            return RedirectToAction("Login", "Account", new { lang = CurrentLanguage });
        }

        private void EmptyLoginInfo()
        {
            string redisKey = $"{Configs.PlateForm}:AccountInfo:{AccountInfo.Id}";
            RedisHelper.KeyDelete(Configs.RedisIndex_Web, redisKey);
            var cookie = Response.Cookies["_LoginInfo"];
            cookie.Expires = DateTime.Now;
        }

        #region 帮助程序

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home", new { lang = CurrentLanguage });
        }
        #endregion




















        #region old logic start

        [HttpPost, AllowAnonymous]
        public async Task<string> CheckAccountStatus(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                string resultmessage = "";
                foreach (string error in ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)))
                    resultmessage += error + Environment.NewLine;
                return new ResponesResult(false, resultmessage).ToString();
            }
            var account = await new AccountComponent().GetAccountByUsernameAsync(model.MerchantId);
            if (account == null)
                return new ResponesResult(false, AccountLogin.MerchantIdNotExist).ToString();

            if (account.Status == (byte)AccountStatus.Registered)
            {
                return new ResponesResult(false, "").ToString();
            }
            return new ResponesResult(true).ToString();
        }

        [AllowAnonymous]
        public ActionResult FbPsw(string merchantId)
        {
            ViewBag.MerchantId = merchantId;
            return View();
        }

        [HttpPost, AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FbPsw(FindBackPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                SecurityVerify.Verify<FbPswEmailVerification>(model.Email.Replace("@", "_"), null, model.Code);
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", GeneralResource.SaveFailed);
                return View(model);
            }
            var accountComponent = new AccountComponent();
            var account = await accountComponent.GetAccountByEmailAsync(model.Email);
            if (account == null)
            {
                ModelState.AddModelError("", AccountFbPsw.EmailNotBind);
                return View(model);
            }
            if (account.Status != (byte)AccountStatus.Active)
            {
                ViewBag.PasswordHasSet = -1;
                return View(model);
            }
            await accountComponent.ResetPasswordAsync(account.Id, PasswordHasher.HashPassword(model.Password));
            ViewBag.PasswordHasSet = 1;
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<string> SendFbPswEmailCode(SendEmailCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                string resultmessage = "";
                foreach (string error in ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)))
                    resultmessage += error + Environment.NewLine;
                return new ResponesResult(false, resultmessage).ToString();
            }
            var account = await new AccountComponent().GetAccountByEmailAsync(model.Email);
            if (account == null)
            {
                return new ResponesResult(false, AccountFbPsw.EmailNotBind).ToString();
            }
            var code = SecurityVerify.SendCode<FbPswEmailVerification>(model.Email.Replace("@", "_"), model.Email);
            if (string.IsNullOrEmpty(code))//一分钟内发送过
                return new ResponesResult(false, GeneralResource.OptionTooFrequent).ToString();
            var subject = AccountFbPsw.EmailSubject;
            var content = string.Format(AccountFbPsw.EmailContent, code);
            await new EmailAgent().SendAsync(model.Email, subject, content);
            return new ResponesResult(true).ToString();
        }

        /// <summary>
        /// 第一次登录设置
        /// </summary>
        /// <returns></returns>
        [FirstLoginOnly]
        public ActionResult FirstSetting()
        {
            return View();
        }

        [HttpPost]
        [FirstLoginOnly]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FirstSetting(FirstSettingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                SecurityVerify.Verify<FirstSettingEmailVerification>(model.Email.Replace("@", "_"), null, model.Code);
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", GeneralResource.SaveFailed);
                return View(model);
            }

            var enPassword = PasswordHasher.HashPassword(model.Password);
            var result = await new AccountComponent().SetPasswordAndEmailAsync(AccountInfo.Id, enPassword, model.Email, (byte)AccountStatus.Active);

            if (!result.Status)
            {
                ModelState.AddModelError("", result.GetMessage());
                return View(model);
            }

            EmptyLoginInfo();

            ViewBag.PasswordHasSet = "1";

            return View(model);
        }

        [HttpPost]
        [FirstLoginOnly]
        public async Task<string> SendFirstSettingEmailCode(SendEmailCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                string resultmessage = "";
                foreach (string error in ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)))
                    resultmessage += error + Environment.NewLine;
                return new ResponesResult(false, resultmessage).ToString();
            }
            if (await new AccountComponent().CheckEmailBind(AccountInfo.Id, model.Email))
                return new ResponesResult(false, AccountFirstSetting.EmailBindByOtherAccount).ToString();
            var code = SecurityVerify.SendCode<FirstSettingEmailVerification>(model.Email.Replace("@", "_"), model.Email);
            if (string.IsNullOrEmpty(code))//一分钟内发送过
                return new ResponesResult(false, GeneralResource.OptionTooFrequent).ToString();
            var subject = AccountFirstSetting.EmailSubject;
            var content = string.Format(AccountFirstSetting.EmailContent, code);
            await new EmailAgent().SendAsync(model.Email, subject, content);
            return new ResponesResult(true).ToString();
        }

        public ActionResult CheckEmail()
        {
            ViewBag.PageName = AccountResetEmail.PageName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckEmail(ResetEmailOldViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                SecurityVerify.Verify<ResetEmailOldVerification>(model.Email.Replace("@", "_"), null, model.Code);
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", GeneralResource.SaveFailed);
                return View(model);
            }

            var emailToken = SecurityVerify.SendCode<ResetEmailOldTokenVerification>(model.Email.Replace("@", "_"), model.Email);
            var timeTicks = Encrypts.GenerateTicksInTenTime();
            var token = HttpUtility.UrlEncode(PasswordHasher.HashPassword(emailToken + timeTicks));

            return RedirectToAction("ResetEmail", new { token });
        }

        [HttpPost]
        public async Task<string> SendCheckEmailCode(SendEmailCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                string resultmessage = "";
                foreach (string error in ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)))
                    resultmessage += error + Environment.NewLine;
                return new ResponesResult(false, resultmessage).ToString();
            }
            var account = await new AccountComponent().GetAccountByIdAsync(AccountInfo.Id);
            if (!account.Email.Equals(model.Email, StringComparison.CurrentCultureIgnoreCase))
            {
                return new ResponesResult(false, AccountResetEmail.EmailNotMatch).ToString();
            }
            var code = SecurityVerify.SendCode<ResetEmailOldVerification>(account.Email.Replace("@", "_"), account.Email);
            if (string.IsNullOrEmpty(code))//一分钟内发送过
                return new ResponesResult(false, GeneralResource.OptionTooFrequent).ToString();
            var subject = AccountResetEmail.EmailSubject;
            var content = string.Format(AccountResetEmail.OriginalEmailContent, code);
            await new EmailAgent().SendAsync(model.Email, subject, content);
            return new ResponesResult(true).ToString();
        }

        public ActionResult ResetEmail(string token)
        {
            ResetEmailNewViewModel model = new ResetEmailNewViewModel { Token = token };
            ViewBag.PageName = AccountResetEmail.PageName;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetEmail(ResetEmailNewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var account = await new AccountComponent().GetAccountByIdAsync(AccountInfo.Id);
            if (model.Email.Equals(account.Email, StringComparison.CurrentCultureIgnoreCase))
            {
                ModelState.AddModelError("", AccountResetEmail.EmailSameAsOriginal);
            }
            try
            {
                var token = HttpUtility.UrlDecode(model.Token);
                SecurityVerify.Verify<ResetEmailOldTokenVerification>(account.Email.Replace("@", "_"), null, token, false);
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", GeneralResource.SaveFailed);
                return View(model);
            }
            try
            {
                SecurityVerify.Verify<ResetEmailNewVerification>(model.Email.Replace("@", "_"), null, model.Code);
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", GeneralResource.SaveFailed);
                return View(model);
            }

            var emailExist = await new AccountComponent().CheckEmailBind(AccountInfo.Id, model.Email);
            if (emailExist)
            {
                ModelState.AddModelError("", AccountFirstSetting.EmailBindByOtherAccount);
                return View(model);
            }

            SecurityVerify.DeleteCodeKey<ResetEmailOldTokenVerification>(account.Email.Replace("@", "_"));

            await new AccountComponent().ResetEmailAsync(account.Id, model.Email);

            return RedirectToAction("Index", "Merchant", new { lang = CurrentLanguage });
        }

        [HttpPost]
        public async Task<string> SendResetEmailCode(SendEmailCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                string resultmessage = "";
                foreach (string error in ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)))
                    resultmessage += error + Environment.NewLine;
                return new ResponesResult(false, resultmessage).ToString();
            }
            var account = await new AccountComponent().GetAccountByIdAsync(AccountInfo.Id);
            if (model.Email.Equals(account.Email, StringComparison.CurrentCultureIgnoreCase))
                return new ResponesResult(false, AccountResetEmail.EmailSameAsOriginal).ToString();

            var emailExist = await new AccountComponent().CheckEmailBind(AccountInfo.Id, model.Email);
            if (emailExist)
                return new ResponesResult(false, AccountFirstSetting.EmailBindByOtherAccount).ToString();

            var code = SecurityVerify.SendCode<ResetEmailNewVerification>(model.Email.Replace("@", "_"), model.Email);
            if (string.IsNullOrEmpty(code))//一分钟内发送过
                return new ResponesResult(false, GeneralResource.OptionTooFrequent).ToString();
            var subject = AccountResetEmail.EmailSubject;
            var content = string.Format(AccountResetEmail.NewEmailContent, code);
            await new EmailAgent().SendAsync(model.Email, subject, content);
            return new ResponesResult(true).ToString();
        }

        #endregion  old logic end

    }
}