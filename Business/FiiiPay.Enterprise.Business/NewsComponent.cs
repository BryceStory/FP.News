using FiiiPay.Enterprise.Data;
using FiiiPay.Enterprise.DTO;
using FiiiPay.Enterprise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FiiiPay.Enterprise.Business
{
    public class NewsComponent
    {
        private NewsDAC _dac = new NewsDAC();

        #region news start
        public List<News> GetRecordList(string title, byte? status, Guid accountId, ref Tuple<string, string, int, int, int, int> pager)
        {
            return new NewsDAC().GetPagedList(title, status, accountId, ref pager);
        }

        public News GetById(int id)
        {
            return new NewsDAC().GetById(id);
        }

        public SaveResult Unpublished(int id)
        {
            var result = new NewsDAC().Unpublished(id);
            return new SaveResult(result);
        }

        public SaveResult Published(int id)
        {
            var result = new NewsDAC().Published(id);
            return new SaveResult(result);
        }

        public SaveResult Delete(int id)
        {
            var result = new NewsDAC().Delete(id);
            return new SaveResult(result);
        }


        public SaveResult Create(News model)
        {
            if (CheckCNTitleExt( model))
            {
                return new SaveResult(false, "This news already exists!");
            }
            if (CheckENTitleExt(model))
            {
                return new SaveResult(false, "This news already exists!");
            }

            return new SaveResult(new NewsDAC().Create(model));
        }

        public SaveResult UpdateCNVersion(News model)
        {
            if (CheckCNTitleExt(model))
            {
                return new SaveResult(false, "This news already exists!");
            }
            if (CheckENTitleExt(model))
            {
                return new SaveResult(false, "This news already exists!");
            }

            var result = new NewsDAC().UpdateCNVersion(model);

            return new SaveResult(result);
        }

        public SaveResult UpdateENVersion(News model)
        {
            if (CheckCNTitleExt(model))
            {
                return new SaveResult(false, "This news already exists!");
            }
            if (CheckENTitleExt(model))
            {
                return new SaveResult(false, "This news already exists!");
            }

            var result = new NewsDAC().UpdateENVersion(model);

            return new SaveResult(result);
        }

        public bool CheckCNTitleExt(News model)
        {
            News ns = new NewsDAC().CheckByCNTitle(model);
            return ns != null;
        }
        public bool CheckENTitleExt(News model)
        {
            News ns = new NewsDAC().CheckByENTitle(model);
            return ns != null;
        }

        #endregion



        #region NewsSection start

        public List<NewsSetion> GetNewsSectionList()
        {
            return new NewsDAC().GetNewsSectionList().ToList();
        }

        public NewsSetion GetNewsSectionById(int id)
        {
            return new NewsDAC().GetNewsSectionById(id);
        }

        public List<NewsSetion> GetSectionRecordList(string title, ref Tuple<string, string, int, int, int, int> pager)
        {
            return new NewsDAC().GetSectionRecordList(title, ref pager);
        }

        public SaveResult CreateNewsSection(NewsSetion model)
        {
            var result = new NewsDAC().CreateNewsSection(model);

            return new SaveResult(result);
        }

        public SaveResult UpdateNewsSection(NewsSetion model)
        {
            var result = new NewsDAC().UpdateNewsSection(model);

            return new SaveResult(result);

        }
        public SaveResult DeleteNewsSection(int id)
        {
            var result = new NewsDAC().DeleteNewsSectionById(id);
            return new SaveResult(result);
        }

        #endregion 



        public List<NewsListOM> GetNewsList(int? newsSetionId, int pageSize, int pageIndex)
        {
            var list = new NewsDAC().GetNewsList(newsSetionId, pageSize, pageIndex);

            return list.Select(t => new NewsListOM
            {
                Id = t.Id,
                CNTitle = t.CNTitle,
                ENTitle = t.ENTitle,
                CNContent = t.CNContent,
                ENContent = t.ENContent,
                CNSource = t.CNSource,
                ENSource = t.ENSource,
                CNKeyword1 = t.CNKeyword1,
                CNKeyword2 = t.CNKeyword2,
                ENKeyword1 = t.ENKeyword1,
                ENKeyword2 = t.ENKeyword2,
                CNCoverPictureUrl = t.CNCoverPictureUrl,
                ENCoverPictureUrl = t.ENCoverPictureUrl,
                CreateTime = t.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Status = (int)t.Status,
                NewsSetionId = t.NewsSetionId,
                CNPageView = t.CNPageView,
                ENPageView = t.ENPageView,
                CNPublisher = t.CNPublisher == null ? "" : t.CNPublisher,
                ENPublisher = t.ENPublisher == null ? "" : t.ENPublisher,
                CNIntro = t.CNIntro == null ? "" : t.CNIntro,
                ENIntro = t.ENIntro == null ? "" : t.ENIntro

            }).ToList();
        }

        public List<NewsSectionOM> GetAllSectionList(int pageSize, int pageIndex)
        {
            var list = new NewsDAC().GetAllSectionList(pageSize, pageIndex);

            return list.Select(t => new NewsSectionOM
            {
                Id = t.Id,
                CNName = t.CNName,
                ENName = t.ENName,
                CreateTime = t.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Sort = t.Sort
            }).ToList();
        }

        public NewsOM CheckNews(int id)
        {
            var news = new NewsDAC().GetById(id);

            return new NewsOM
            {
                Id = news.Id,
                CNTitle = news.CNTitle,
                ENTitle = news.ENTitle,
                CNContent = news.CNContent,
                ENContent = news.ENContent,
                CNSource = news.CNSource,
                ENSource = news.ENSource,
                CNKeyword1 = news.CNKeyword1,
                CNKeyword2 = news.CNKeyword2,
                ENKeyword1 = news.ENKeyword1,
                ENKeyword2 = news.ENKeyword2,
                CNCoverPictureUrl = news.CNCoverPictureUrl,
                ENCoverPictureUrl = news.ENCoverPictureUrl,
                CreateTime = news.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Status = (int)news.Status,
                CNPageView = news.CNPageView,
                ENPageView = news.ENPageView,
                CNPublisher = news.CNPublisher == null ? "" : news.CNPublisher,
                ENPublisher = news.ENPublisher == null ? "" : news.ENPublisher,
                CNIntro = news.CNIntro == null ? "" : news.CNIntro,
                ENIntro = news.ENIntro == null ? "" : news.ENIntro
            };
        }

        public async Task<PageViewOM> PageViewAdd(int newsId, int version)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var beforeCNPageView = await _dac.GetCNPageViewByIdAsync(newsId);
                var beforeENPageView = await _dac.GetENPageViewByIdAsync(newsId);


                if (version == (int)NewsVersionEnum.CNVersion)
                {
                    await _dac.UpdateCNPageViewByIdAsync(newsId, beforeCNPageView + 1);
                }
                else if (version == (int)NewsVersionEnum.ENVersion)
                {
                    await _dac.UpdateENPageViewByIdAsync(newsId, beforeENPageView + 1);
                }

                var currCNPageView = await _dac.GetCNPageViewByIdAsync(newsId);
                var currENPageView = await _dac.GetENPageViewByIdAsync(newsId);

                scope.Complete();
                return new PageViewOM { CNPageView = currCNPageView, ENPageView = currENPageView };
            }
        }

    }
}
