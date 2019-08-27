using FiiiPay.Enterprise.Business;
using FiiiPay.Enterprise.DTO;
using FiiiPay.Enterprise.Web.Models;
using FiiiPay.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiiiPay.Enterprise.Web.Controllers
{
    [Access]
    public class RateController : BaseController
    {
        private PriceInfoHistoryComponent _component = new PriceInfoHistoryComponent();
        private string cryptoList = System.Configuration.ConfigurationManager.AppSettings["CryptoList"];
        private string fiatcurrency = System.Configuration.ConfigurationManager.AppSettings["Fiatcurrency"];

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetPriceInfo()
        {
            ServiceResult<List<PriceInfoHistoryOM>> result = new ServiceResult<List<PriceInfoHistoryOM>>
            {
                Data = _component.GetPriceList(cryptoList,fiatcurrency)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}