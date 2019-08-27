using FiiiPay.Enterprise.Business;
using FiiiPay.Enterprise.Web.Models;
using FiiiPay.Enterprise.Web.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiiiPay.Enterprise.Web.Controllers
{
    [Access]
    public class MerchantController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.PageName = MerchantIndex.Pagename;
            var account = new AccountComponent().GetAccountById(AccountInfo.Id);
            BaseInfomationModel info = new BaseInfomationModel
            {
                MerchantId = account.Username,
                Email = account.Email,
                Password = "********"
            };
            return View(info);
        }
    }
}