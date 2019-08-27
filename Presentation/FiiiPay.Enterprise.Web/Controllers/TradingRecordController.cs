using FiiiPay.Enterprise.Business;
using FiiiPay.Enterprise.Entities;
using FiiiPay.Enterprise.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FiiiPay.Enterprise.Web.Controllers
{
    [Access]
    public class TradingRecordController : BaseController
    {
        // GET: TradingRecord
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult LoadData(OrderViewModel order, GridPager pager)
        {
            var pagerTuple = Tuple.Create(pager.SortColumn, pager.OrderBy, pager.Page, pager.Size, 0, 0);
            var result = new TradingRecordComponent().GetRecordList(order.OrderNo, order.UserName, order.Status, ref pagerTuple);
            pager.Count = pagerTuple.Item5;
            pager.TotalPage = pagerTuple.Item6;
            var models = result.ToGridJson(pager, item => new
            {
                OrderNo = item.OrderNo,
                UserName = item.UserName,
                Amount = item.Amount,
                Timestamp = item.Timestamp.ToString("yyyy-MM-dd HH:mm"),
                Status = item.Status
            });
            return Json(models, JsonRequestBehavior.AllowGet);
        }
    }
}