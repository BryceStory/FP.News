using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiiiPay.Enterprise.Web.Controllers
{
    public class WalletController : Controller
    {
        // GET: Wallet
        public ActionResult MyBalance()
        {
            return View();
        }

        public ActionResult WithdrawalList()
        {
            return View();
        }
    }
}