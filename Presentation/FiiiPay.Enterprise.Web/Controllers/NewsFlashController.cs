using FiiiPay.Enterprise.Business;
using FiiiPay.Enterprise.Web.Models;
using FiiiPay.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiiiPay.Enterprise.DTO;
using FiiiPay.Enterprise.Entities;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Web.Controllers
{
    [Access]
    public class NewsFlashController : BaseController
    {
        private NewsFlashComponent _component = new NewsFlashComponent();
        public ActionResult Index()
        {
            return View();
        }



        public JsonResult LoadData(NewsFlashModel news, GridPager pager)
        {
            var pagerTuple = Tuple.Create(pager.SortColumn, pager.OrderBy, pager.Page, pager.Size, 0, 0);
            var result = new NewsFlashComponent().GetRecordList(news.Title, news.Status, ref pagerTuple);
            pager.Count = pagerTuple.Item5;
            pager.TotalPage = pagerTuple.Item6;

            var models = result.ToGridJson(pager, item => new
            {
                Id = item.Id,
                CNTitle = item.CNTitle == null ? "" : item.CNTitle,
                ENTitle = item.ENTitle == null ? "" : item.ENTitle,
                CNPageView = item.CNPageView == null ? 0 : item.CNPageView,
                ENPageView = item.ENPageView == null ? 0 : item.ENPageView,
                Status = item.Status,
                CreateTime = item.CreateTime.AddHours(8).ToString("yyyy-MM-dd HH:mm"),
                Operate = item.Status

            });

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            NewsFlash news = new NewsFlash();
            return View(news);
        }

        public ActionResult Edit(int Id)
        {
            NewsFlash nf = new NewsFlash();
            nf.Id = -1;
            if (Id > 0)
            {
                nf = _component.GetById(Id);
            }
            ViewBag.CoverPictureList = _component.GetFlashFilesById(Id);

            return View(nf);
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
            return Json(_component.Delete(id).toJson());
        }


        public PartialViewResult UploadPicture()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SavePublishFlash(NewsFlashEditModel model)
        {
            SaveResult result = new SaveResult();
            if (model.Id > 0)//编辑
            {
                if (model.Version == (int)FlashVersion.CN)
                {
                    result = _component.UpdateCNVersion(new NewsFlash
                    {
                        CNGood = model.CNGood,
                        CNBad = model.CNBad,

                        CNTitle = model.CNTitle,
                        CNContent = model.CNContent,
                        CNSource = model.CNSource,
                        Status = (NewsFlashStatus)model.Status,
                        Id = model.Id

                    }, model.CNCoverPictureUrlList, model.Version);

                }
                if (model.Version == (int)FlashVersion.EN)
                {
                    result = _component.UpdateENVersion(new NewsFlash
                    {
                        ENGood = model.ENGood,
                        ENBad = model.ENBad,
                        ENTitle = model.ENTitle,
                        ENContent = model.ENContent,
                        ENSource = model.ENSource,
                        Status = (NewsFlashStatus)model.Status,
                        Id = model.Id
                    }, model.ENCoverPictureUrlList, model.Version);

                }
            }
            else //新增
            {
                NewsFlash flash = new NewsFlash
                {
                    CNTitle = model.CNTitle,
                    ENTitle = model.ENTitle,
                    CNContent = model.CNContent,
                    ENContent = model.ENContent,
                    CNSource = model.CNSource,
                    ENSource = model.ENSource,
                    Status = (NewsFlashStatus)model.Status,
                    AccountsId = AccountInfo.Id,
                    CNPageView = 0,
                    ENPageView = 0,
                    CreateTime = DateTime.UtcNow,
                    ModifyTime = null,

                    CNGood = (model.Version == (int)FlashVersion.CN ? model.CNGood : model.ENGood),
                    CNBad = (model.Version == (int)FlashVersion.CN ? model.CNBad : model.ENBad),
                    ENGood = (model.Version == (int)FlashVersion.EN ? model.ENGood : model.CNGood),
                    ENBad = (model.Version == (int)FlashVersion.EN ? model.ENBad : model.CNBad)

                };
                if (model.Version == (int)FlashVersion.CN)
                {
                    result = new NewsFlashComponent().Create(flash, model.CNCoverPictureUrlList, model.Version);
                }
                else
                {
                    result = new NewsFlashComponent().Create(flash, model.ENCoverPictureUrlList, model.Version);
                }

            }
            return Json(result.toJson());
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult NewsFlashList(NewsFlashListModel model)
        {
            ServiceResult<List<NewsFlashListOM>> result = new ServiceResult<List<NewsFlashListOM>>
            {
                Data = _component.GetNewsFlashList(model.PageSize.Value, model.PageIndex.Value)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Check(NewsDetailModel model)
        {
            ServiceResult<NewsFlashOM> result = new ServiceResult<NewsFlashOM>
            {
                Data = _component.CheckNewsFlash(model.Id)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 只有利好利空的点击
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> GoodBadSelect(GoodSelectModel model)
        {
            ServiceResult<GoodAndBadInfoOM> result = new ServiceResult<GoodAndBadInfoOM>
            {
                Data = await _component.GoodSelect(model.Id, model.SelectStatus)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}