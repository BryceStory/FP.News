using FiiiPay.Enterprise.Business;
using FiiiPay.Enterprise.DTO;
using FiiiPay.Enterprise.Entities;
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
    public class BannerController : BaseController
    {
        private string Pathbase = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];//图片保存路径
        private BannerComponent _component = new BannerComponent();

        #region 首页banner
        public ActionResult HomepageBannerIndex()
        {
            return View();
        }

        public JsonResult LoadHomePageData(BannerQueryModel banner, GridPager pager)
        {
            var pagerTuple = Tuple.Create(pager.SortColumn, pager.OrderBy, pager.Page, pager.Size, 0, 0);
            var result = _component.GetHomePageRecordList(banner.Title, banner.Version, ref pagerTuple);
            pager.Count = pagerTuple.Item5;
            pager.TotalPage = pagerTuple.Item6;

            var models = result.ToGridJson(pager, item => new
            {
                Id = item.Id,
                Title = item.Title == null ? "" : item.Title,
                Sort = item.Sort,
                Version = item.Version == BannerVersion.CN ? "中文版" : "英文版",
                Status = item.Status,
                CreateTime = item.CreateTime.AddHours(8).ToString("yyyy-MM-dd HH:mm"),
                Operate = item.Status

            });

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HomepageBanner(int Id)
        {
            Banner banner = new Banner();
            banner.Id = -1;
            if (Id > 0)
            {
                banner = _component.GetById(Id);
            }
            ViewBag.BannerPictureList = _component.GetBannerFileByBannerId(Id);

            return View(banner);
        }

        public PartialViewResult UploadHPBanner()
        {
            return PartialView();
        }

        #endregion 首页banner


        #region Fiii生态Banner
        public ActionResult FiiiBannerIndex()
        {
            return View();
        }

        public JsonResult LoadFiiiData(BannerQueryModel banner, GridPager pager)
        {
            var pagerTuple = Tuple.Create(pager.SortColumn, pager.OrderBy, pager.Page, pager.Size, 0, 0);
            var result = _component.GetFiiiRecordList(banner.Title, banner.Version, ref pagerTuple);
            pager.Count = pagerTuple.Item5;
            pager.TotalPage = pagerTuple.Item6;

            var models = result.ToGridJson(pager, item => new
            {
                Id = item.Id,
                Title = item.Title == null ? "" : item.Title,
                Sort = item.Sort,
                Version = item.Version == BannerVersion.CN ? "中文版" : "英文版",
                Status = item.Status,
                CreateTime = item.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                Operate = item.Status

            });

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FiiiBanner(int Id)
        {
            Banner banner = new Banner();
            banner.Id = -1;
            if (Id > 0)
            {
                banner = _component.GetById(Id);
            }
            ViewBag.BannerPictureList = _component.GetBannerFileByBannerId(Id);

            return View(banner);
        }

        public PartialViewResult UploadFiiiBanner()
        {
            return PartialView();
        }

        #endregion Fiii生态Banner


        #region 广告
        public ActionResult NewsBannerIndex()
        {
            return View();
        }

        public JsonResult LoadNewsAdvData(BannerQueryModel banner, GridPager pager)
        {
            var pagerTuple = Tuple.Create(pager.SortColumn, pager.OrderBy, pager.Page, pager.Size, 0, 0);
            var result = _component.GetNewsAdvRecordList(banner.Title, banner.Version, ref pagerTuple);
            pager.Count = pagerTuple.Item5;
            pager.TotalPage = pagerTuple.Item6;

            var models = result.ToGridJson(pager, item => new
            {
                Id = item.Id,
                Title = item.Title == null ? "" : item.Title,
                Sort = item.Sort,
                Version = item.Version == BannerVersion.CN ? "中文版" : "英文版",
                Status = item.Status,
                CreateTime = item.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                Operate = item.Status
            });

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewsAdvBanner(int Id)
        {
            Banner banner = new Banner();
            banner.Id = -1;
            if (Id > 0)
            {
                banner = _component.GetById(Id);
            }
            ViewBag.BannerPictureList = _component.GetBannerFileByBannerId(Id);

            return View(banner);
        }

        public PartialViewResult UploadNewsAdcBanner()
        {
            return PartialView();
        }

        #endregion 广告


        [HttpPost]
        public JsonResult SaveEdit(BannerEditModel model)
        {
            SaveResult result = new SaveResult();
            if (model.Id > 0) //编辑
            {
                Banner banner = new Banner
                {
                    Id = model.Id,
                    AccountId = AccountInfo.Id,
                    Title = model.Title,
                    LinkUrl = model.LinkUrl == null ? "#" : model.LinkUrl,
                    Version = model.Version == 1 ? BannerVersion.CN : BannerVersion.EN,
                    Status = BannerStatus.Published,
                    Type = model.Type == 0 ? BannerType.HomePageBanner : (model.Type == 1 ? BannerType.FiiiEcologyBanner : BannerType.NewsBanner),
                    CreateTime = DateTime.UtcNow,
                    ModifyTime = null,
                    Sort = model.Sort
                };
                result = _component.UpdateBanner(banner, model.BannerFileUrlList);
            }
            else  //新增
            {
                Banner banner = new Banner
                {
                    AccountId = AccountInfo.Id,
                    Title = model.Title,
                    LinkUrl = model.LinkUrl == null ? "#" : model.LinkUrl,
                    Version = model.Version == 1 ? BannerVersion.CN : BannerVersion.EN,
                    Status = BannerStatus.Published,
                    Type = model.Type == 0 ? BannerType.HomePageBanner : (model.Type == 1 ? BannerType.FiiiEcologyBanner : BannerType.NewsBanner),
                    CreateTime = DateTime.UtcNow,
                    ModifyTime = null,
                    Sort = model.Sort
                };
                result = _component.InsertBanner(banner, model.BannerFileUrlList);
            }

            return Json(result.toJson());
        }

        public ActionResult Unpublished(int id)
        {
            return Json(_component.Unpublished(id).toJson());
        }

        public ActionResult Published(int id)
        {
            return Json(_component.Published(id).toJson());
        }

        public ActionResult Delete(int id)
        {
            return Json(_component.DeleteById(id).toJson());
        }




        [HttpPost]
        [AllowAnonymous]
        public ActionResult BannerList(BannerModel model)
        {
            ServiceResult<List<BannerListOM>> result = new ServiceResult<List<BannerListOM>>
            {
                Data = _component.GetBannerListByType(model.PageSize.Value, model.PageIndex.Value, model.Type, model.Version)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Check(BannerDetailModel model)
        {
            ServiceResult<BannerOM> result = new ServiceResult<BannerOM>
            {
                Data = _component.CheckBanner(model.Id)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}