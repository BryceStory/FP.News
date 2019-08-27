using FiiiPay.Enterprise.Web.Models;
using FiiiPay.Enterprise.Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace FiiiPay.Enterprise.Web.Controllers
{
    [Access]
    public class HomeController : BaseController
    {
        public ActionResult Index(string lang)
        {
            return View();
        }

        public JsonResult LoadData(GridPager pager,string Condition1)
        {
            List<HomeIndexModel> models = new List<HomeIndexModel>();
            for (int i = 0; i < 120; i++)
            {
                models.Add(new HomeIndexModel
                {
                    Content1 = $"Content{i}1",
                    Content2 = $"Content{i}2",
                    Content3 = $"Content{i}3",
                });
            }
            if(!string.IsNullOrEmpty(Condition1))
                models = models.FindAll(t => t.Content1.Contains(Condition1));
            var data = models.ToGridJson(ref pager, null, false);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}