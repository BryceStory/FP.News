using FiiiPay.Enterprise.Business;
using FiiiPay.Enterprise.Entities;
using FiiiPay.Enterprise.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FiiiPay.Enterprise.Web.Controllers
{
    [Access]
    public class AccountRoleController : BaseController
    {
        private AccountRoleComponent _component = new AccountRoleComponent();

        public ActionResult Index()
        {
            return View();
        }


        public JsonResult LoadRoleData(RoleSearchModel model, GridPager pager)
        {
            var pagerTuple = Tuple.Create(pager.SortColumn, pager.OrderBy, pager.Page, pager.Size, 0, 0);
            var result = _component.GetRoleRecordList(model.RoleName, ref pagerTuple);
            pager.Count = pagerTuple.Item5;
            pager.TotalPage = pagerTuple.Item6;

            var models = result.ToGridJson(pager, item => new
            {
                Id = item.Id,
                RoleName = item.Name,
                Description = item.Description == null ? "" : item.Description,
                CreateTime = item.CreateTime.AddHours(8).ToString("yyyy-MM-dd HH:mm"),
                Authorize = item.Id,
                Operate = item.Id

            });

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View("Edit");
        }

        public ActionResult Edit(int Id)
        {
            AccountRole role = _component.GetById(Id);
            return View(role);
        }

        [HttpPost]
        public ActionResult Save(AccountRole role)
        {
            if (role.Id == 0)  //新增
            {
                return Json(SaveCreate(role).toJson());
            }
            else  //修改
            {
                return Json(SaveEdit(role).toJson());
            }
        }

        private SaveResult SaveCreate(AccountRole role)
        {
            role.CreateBy = AccountInfo.Id;
            role.CreateTime = DateTime.UtcNow;

            return _component.CreateRole(role);
        }

        private SaveResult SaveEdit(AccountRole role)
        {
            role.ModifyBy = AccountInfo.Id;
            role.ModifyTime = DateTime.UtcNow;

            return _component.UpdateRole(role);
        }

        public ActionResult DeleteRole(int id)
        {
            return Json(_component.DeleteRole(id).toJson());
        }
    }
}