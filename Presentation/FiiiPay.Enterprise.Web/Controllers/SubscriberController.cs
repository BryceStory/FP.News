using FiiiPay.Enterprise.Business;
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
    public class SubscriberController : BaseController
    {
        private SubscriberComponent _component = new SubscriberComponent();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadData(SubscriberViewModel model, GridPager pager)
        {
            var pagerTuple = Tuple.Create(pager.SortColumn, pager.OrderBy, pager.Page, pager.Size, 0, 0);
            var result = _component.GetRecordList(model.Email, ref pagerTuple);
            pager.Count = pagerTuple.Item5;
            pager.TotalPage = pagerTuple.Item6;

            var models = result.ToGridJson(pager, item => new
            {
                Id = item.Id,
                Email = item.Email,
                CreateTime = item.CreateTime.AddHours(8).ToString("yyyy-MM-dd HH:mm")
            });

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Insert(SubscriberInsertModel model)
        {
            ServiceResult<bool> result = new ServiceResult<bool>
            {
                Data = _component.Insert(model.Email)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}