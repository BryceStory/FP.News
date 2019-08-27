using FiiiPay.Enterprise.Business;
using FiiiPay.Enterprise.DTO;
using FiiiPay.Enterprise.Entities;
using FiiiPay.Enterprise.Web.Models;
using FiiiPay.Enterprise.Web.Utils;
using FiiiPay.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FiiiPay.Enterprise.Web.Controllers
{
    [Access]
    public class NewsController : BaseController
    {
        private NewsComponent _component = new NewsComponent();

        #region news start
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadData(NewsViewModel news, GridPager pager)
        {
            var pagerTuple = Tuple.Create(pager.SortColumn, pager.OrderBy, pager.Page, pager.Size, 0, 0);
            var result = _component.GetRecordList(news.Title, news.Status, AccountInfo.Id, ref pagerTuple);
            pager.Count = pagerTuple.Item5;
            pager.TotalPage = pagerTuple.Item6;

            var models = result.ToGridJson(pager, item => new
            {
                Id = item.Id,
                CNTitle = item.CNTitle == null ? "" : item.CNTitle,
                ENTitle = item.ENTitle == null ? "" : item.ENTitle,
                CNPageView = item.CNPageView,
                ENPageView = item.ENPageView,
                Status = item.Status,
                CreateTime = item.CreateTime.AddHours(8).ToString("yyyy-MM-dd HH:mm"),
                Operate = item.Status

            });

            return Json(models, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Edit(int Id)
        {
            News news = new News();
            news.Id = -1;
            if (Id > 0)
            {
                news = _component.GetById(Id);
            }

            List<NewsSetion> list = _component.GetNewsSectionList();
            List<SelectListItem> CNList = new List<SelectListItem>();
            List<SelectListItem> ENList = new List<SelectListItem>();
            if (list != null)
            {
                CNList.Add(new SelectListItem() { Text = "", Value = "" });
                ENList.Add(new SelectListItem() { Text = "", Value = "" });

                foreach (var item in list)
                {
                    CNList.Add(new SelectListItem() { Text = item.CNName, Value = item.Id.ToString() });
                }

                foreach (var item in list)
                {
                    ENList.Add(new SelectListItem() { Text = item.ENName, Value = item.Id.ToString() });
                }
            }
            ViewBag.CNNewsSectionList = CNList;
            ViewBag.ENNewsSectionList = ENList;
            return View(news);
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SavePublishNews(NewsEditModel model)
        {

            SaveResult result = new SaveResult();
            if (model.Id > 0)//编辑
            {
                if (model.Version == (byte)NewsSubmitFormType.CNVersion)
                {
                    result = _component.UpdateCNVersion(new News
                    {
                        CNIntro = model.CNIntro,
                        CNPublisher = model.CNPublisher,
                        CNPageView = model.CNPageView,
                        CNTitle = model.CNTitle,
                        CNContent = model.CNContent,
                        CNSource = model.CNSource,
                        CNKeyword1 = model.CNKeyword1,
                        CNKeyword2 = model.CNKeyword2,
                        Status = (NewsStatus)model.Status,
                        CNCoverPictureUrl = model.CNCoverPictureUrlList == null ? "" : model.CNCoverPictureUrlList[0],
                        Id = model.Id,
                        NewsSetionId = model.NewsSetionId

                    });

                }
                if (model.Version == (byte)NewsSubmitFormType.ENVersion)
                {
                    result = _component.UpdateENVersion(new News
                    {
                        ENIntro = model.ENIntro,
                        ENPublisher = model.ENPublisher,
                        ENPageView = model.ENPageView,
                        ENTitle = model.ENTitle,
                        ENContent = model.ENContent,
                        ENSource = model.ENSource,
                        ENKeyword1 = model.ENKeyword1,
                        ENKeyword2 = model.ENKeyword2,
                        ENCoverPictureUrl = model.ENCoverPictureUrlList == null ? "" : model.ENCoverPictureUrlList[0],
                        Status = (NewsStatus)model.Status,
                        NewsSetionId = model.NewsSetionId,
                        Id = model.Id
                    });

                }
            }
            else //新增
            {
                News news = new News
                {
                    CNIntro = model.CNIntro,
                    ENIntro = model.ENIntro,
                    CNPublisher = model.CNPublisher,
                    ENPublisher = model.ENPublisher,
                    CNTitle = model.CNTitle,
                    ENTitle = model.ENTitle,
                    CNPageView = model.CNPageView,
                    ENPageView = model.ENPageView,
                    CNContent = model.CNContent,
                    ENContent = model.ENContent,
                    CNSource = model.CNSource,
                    ENSource = model.ENSource,
                    CNKeyword1 = model.CNKeyword1,
                    ENKeyword1 = model.ENKeyword1,
                    CNKeyword2 = model.CNKeyword2,
                    ENKeyword2 = model.ENKeyword2,
                    Status = (NewsStatus)model.Status,
                    CreateTime = DateTime.UtcNow,
                    ModifyTime = null,
                    AccountsId = AccountInfo.Id,
                    CNCoverPictureUrl = model.CNCoverPictureUrlList == null ? "" : model.CNCoverPictureUrlList[0],
                    ENCoverPictureUrl = model.ENCoverPictureUrlList == null ? "" : model.ENCoverPictureUrlList[0],
                    NewsSetionId = model.NewsSetionId
                };
                result = new NewsComponent().Create(news);
            }
            return Json(result.toJson());
        }

        public PartialViewResult UploadPicture()
        {
            return PartialView();
        }
        #endregion




        #region news section start
        public ActionResult Section()
        {
            return View();
        }

        public JsonResult LoadSectionData(NewsSectionViewModel news, GridPager pager)
        {
            var pagerTuple = Tuple.Create(pager.SortColumn, pager.OrderBy, pager.Page, pager.Size, 0, 0);
            var result = _component.GetSectionRecordList(news.Title, ref pagerTuple);
            pager.Count = pagerTuple.Item5;
            pager.TotalPage = pagerTuple.Item6;

            var models = result.ToGridJson(pager, item => new
            {

                CNName = item.CNName == null ? "" : item.CNName,
                ENName = item.ENName == null ? "" : item.ENName,
                Id = item.Id,
                Sort = item.Sort,
                CreateTime = item.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                Operate = item.Status

            });

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SectionEdit(int Id)
        {
            NewsSetion ns = new NewsSetion();
            ns.Id = -1;
            if (Id > 0)
            {
                ns = _component.GetNewsSectionById(Id);
            }
            return View(ns);
        }

        public ActionResult DeleteNewsSection(int id)
        {
            return Json(_component.DeleteNewsSection(id).toJson());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveNewsSection(NewsSectionModel model)
        {
            if (model.Id <= 0) //新增
            {
                var res = _component.CreateNewsSection(new NewsSetion
                {
                    CNName = model.CNName,
                    ENName = model.ENName,
                    Sort = model.Sort,
                    CreateTime = DateTime.UtcNow,
                    Status = 0,
                    AccountsId = AccountInfo.Id
                });
                return Json(res.toJson());
            }
            var resEdit = _component.UpdateNewsSection(new NewsSetion
            {
                Id = model.Id,
                CNName = model.CNName,
                ENName = model.ENName,
                Sort = model.Sort,
                AccountsId = AccountInfo.Id
            });
            return Json(resEdit.toJson());
        }

        #endregion 


        [HttpPost]
        [AllowAnonymous]
        public ActionResult NewsList(NewsModels model)
        {
            ServiceResult<List<NewsListOM>> result = new ServiceResult<List<NewsListOM>>
            {
                Data = _component.GetNewsList(model.NewsSetionId, model.PageSize.Value, model.PageIndex.Value)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult NewsSectionList(SectionModel model)
        {
            ServiceResult<List<NewsSectionOM>> result = new ServiceResult<List<NewsSectionOM>>
            {
                Data = _component.GetAllSectionList(model.PageSize.Value, model.PageIndex.Value)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Check(NewsDetailModel model)
        {
            ServiceResult<NewsOM> result = new ServiceResult<NewsOM>
            {
                Data = _component.CheckNews(model.Id)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> PageViewAdd(PageViewAddModel model)
        {
            ServiceResult<PageViewOM> result = new ServiceResult<PageViewOM>
            {
                Data = await _component.PageViewAdd(model.Id, model.Version)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}