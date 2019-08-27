using FiiiPay.Enterprise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiiiPay.Enterprise.Data;
using FiiiPay.Enterprise.DTO;
using System.Transactions;

namespace FiiiPay.Enterprise.Business
{
    public class NewsFlashComponent
    {
        private NewsFlashDAC _dac = new NewsFlashDAC();
        public List<NewsFlash> GetRecordList(string title, byte? status, ref Tuple<string, string, int, int, int, int> pager)
        {
            return new NewsFlashDAC().GetPagedList(title, status, ref pager);
        }

        public NewsFlash GetById(int id)
        {
            return new NewsFlashDAC().GetById(id);
        }

        public SaveResult Unpublished(int id)
        {
            var result = new NewsFlashDAC().Unpublished(id);
            return new SaveResult(result);
        }

        public SaveResult Published(int id)
        {
            var result = new NewsFlashDAC().Published(id);
            return new SaveResult(result);
        }

        public SaveResult Delete(int id)
        {
            var result = false;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                new NewsFlashDAC().DeleteFlash(id);
                new NewsFlashDAC().DeleteFlashFilesById(id);

                scope.Complete();
                result = true;
            }

            return new SaveResult(result);
        }

        public SaveResult Create(NewsFlash model, string[] pictureUrls, int version)
        {
            if (CheckCNTitleExt(model))
            {
                return new SaveResult(false, "This news flash already exists!");
            }
            if (CheckENTitleExt(model))
            {
                return new SaveResult(false, "This news flash already exists!");
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = new NewsFlashDAC().CreateNewsFlash(model);
                if (pictureUrls != null)
                {
                    new NewsFlashDAC().CreateNewsFlashFiles(result, pictureUrls, version);
                }

                scope.Complete();
            }

            return new SaveResult(true);
        }


        public SaveResult UpdateCNVersion(NewsFlash model, string[] pictureUrls, int version)
        {
            if (CheckCNTitleExt(model))
            {
                return new SaveResult(false, "This news flash already exists!");
            }
            if (CheckENTitleExt(model))
            {
                return new SaveResult(false, "This news flash already exists!");
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                new NewsFlashDAC().UpdateCNVersion(model);
                new NewsFlashDAC().DeleteFlashFilesByIdAndVer(model.Id, version);

                if (pictureUrls != null)
                {
                    new NewsFlashDAC().CreateNewsFlashFiles(model.Id, pictureUrls, version);
                }

                scope.Complete();
            }
            return new SaveResult(true);
        }

        public SaveResult UpdateENVersion(NewsFlash model, string[] pictureIds, int version)
        {
            if (CheckCNTitleExt(model))
            {
                return new SaveResult(false, "This news flash already exists!");
            }
            if (CheckENTitleExt(model))
            {
                return new SaveResult(false, "This news flash already exists!");
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                new NewsFlashDAC().UpdateENVersion(model);
                new NewsFlashDAC().DeleteFlashFilesByIdAndVer(model.Id, version);
                if (pictureIds != null)
                {
                    new NewsFlashDAC().CreateNewsFlashFiles(model.Id, pictureIds, version);
                }

                scope.Complete();
            }
            return new SaveResult(true);
        }

        public bool CheckCNTitleExt(NewsFlash model)
        {
            NewsFlash nf = new NewsFlashDAC().CheckByCNTitle(model);
            return nf != null;
        }
        public bool CheckENTitleExt(NewsFlash model)
        {
            NewsFlash nf = new NewsFlashDAC().CheckByENTitle(model);
            return nf != null;
        }


        public List<NewsFlashListOM> GetNewsFlashList(int pageSize, int pageIndex)
        {
            var nfDAC = new NewsFlashDAC();
            var list = new NewsFlashDAC().GetNewsFlashList(pageSize, pageIndex).ToList();

            var nfList = new List<NewsFlashListOM>();

            for (int i = 0; i < list.Count; i++)
            {
                var temp = new NewsFlashListOM();
                temp.Id = list[i].Id;
                temp.CNTitle = list[i].CNTitle;
                temp.ENTitle = list[i].ENTitle;
                temp.CNContent = list[i].CNContent;
                temp.ENContent = list[i].ENContent;
                temp.CNSource = list[i].CNSource;
                temp.ENSource = list[i].ENSource;
                temp.Status = Convert.ToInt32(list[i].Status);
                temp.CreateTime = list[i].CreateTime.ToString("yyyy-MM-dd HH:mm:ss");

                temp.Good = list[i].CNGood;
                temp.Bad = list[i].CNBad;

                temp.CNCoverPictureUrl = nfDAC.GetFlashFilesById(list[i].Id).Where(v => v.Version == FlashVersion.CN).Select(t => t.CoverPictureUrl).ToList();
                temp.ENCoverPictureUrl = nfDAC.GetFlashFilesById(list[i].Id).Where(v => v.Version == FlashVersion.EN).Select(t => t.CoverPictureUrl).ToList();

                nfList.Add(temp);
            }

            return nfList;
        }

        public List<NewsFlashFiles> GetFlashFilesById(int id)
        {
            var list = new NewsFlashDAC().GetFlashFilesById(id);

            return list;
        }

        public NewsFlashOM CheckNewsFlash(int id)
        {
            var newsFlash = new NewsFlashDAC().GetById(id);
            var newsFlashFileList = new NewsFlashDAC().GetFlashFilesById(id);
            var CNCoverPictureId = newsFlashFileList.Select(t => t.Version == FlashVersion.CN).ToArray();
            var ENCoverPictureId = newsFlashFileList.Select(t => t.Version == FlashVersion.EN).ToArray();
            return new NewsFlashOM
            {
                Id = newsFlash.Id,
                CNTitle = newsFlash.CNTitle,
                ENTitle = newsFlash.ENTitle,
                CNContent = newsFlash.CNContent,
                ENContent = newsFlash.ENContent,
                CNSource = newsFlash.CNSource,
                ENSource = newsFlash.ENSource,
                CNCoverPictureUrl = newsFlashFileList.Where(v => v.Version == FlashVersion.CN).Select(t => t.CoverPictureUrl).ToArray(),
                ENCoverPictureUrl = newsFlashFileList.Where(v => v.Version == FlashVersion.EN).Select(t => t.CoverPictureUrl).ToArray(),
                CreateTime = newsFlash.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Good = newsFlash.CNGood,
                Bad = newsFlash.CNBad,

                Status = (int)newsFlash.Status
            };
        }

        public async Task<GoodAndBadInfoOM> GoodSelect(int newsFlashId, int selectStatus)
        {

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var beforeGoodNum = await _dac.GetGoodByIdAsync(newsFlashId);
                var beforeBadNum = await _dac.GetBadByIdAsync(newsFlashId);

                if (selectStatus == (int)NFGoodBadSelectStatus.GoodOnce)
                {
                    await _dac.UpdateGoodByIdAsync(newsFlashId, beforeGoodNum + 1);
                }
                else if (selectStatus == (int)NFGoodBadSelectStatus.GoodTwice && beforeGoodNum > 0)
                {
                    await _dac.UpdateGoodByIdAsync(newsFlashId, beforeGoodNum - 1);
                }
                else if (selectStatus == (int)NFGoodBadSelectStatus.BadOnce)
                {
                    await _dac.UpdateBadByIdAsync(newsFlashId, beforeBadNum + 1);
                }
                else if (selectStatus == (int)NFGoodBadSelectStatus.BadTwice && beforeBadNum > 0)
                {
                    await _dac.UpdateBadByIdAsync(newsFlashId, beforeBadNum - 1);
                }
                else if (selectStatus == (int)NFGoodBadSelectStatus.FirstGoodThenBad)
                {
                    if (beforeGoodNum > 0)
                    {
                        await _dac.UpdateGoodByIdAsync(newsFlashId, beforeGoodNum - 1);
                    }

                    await _dac.UpdateBadByIdAsync(newsFlashId, beforeBadNum + 1);
                }
                else if (selectStatus == (int)NFGoodBadSelectStatus.FirstBadThenGood)
                {
                    if (beforeBadNum > 0)
                    {
                        await _dac.UpdateBadByIdAsync(newsFlashId, beforeBadNum - 1);
                    }

                    await _dac.UpdateGoodByIdAsync(newsFlashId, beforeGoodNum + 1);
                }

                var currGoodNum = await _dac.GetGoodByIdAsync(newsFlashId);
                var currBadNum = await _dac.GetBadByIdAsync(newsFlashId);

                scope.Complete();
                return new GoodAndBadInfoOM { Good = currGoodNum, Bad = currBadNum };
            }
        }
    }
}
