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
    public class BannerComponent
    {
        public List<Banner> GetHomePageRecordList(string title, int Version, ref Tuple<string, string, int, int, int, int> pager)
        {
            return new BannerDAC().GetHomePageRecordList(title, Version, ref pager);
        }

        public List<Banner> GetFiiiRecordList(string title, int Version, ref Tuple<string, string, int, int, int, int> pager)
        {
            return new BannerDAC().GetFiiiRecordList(title, Version, ref pager);
        }

        public List<Banner> GetNewsAdvRecordList(string title, int Version, ref Tuple<string, string, int, int, int, int> pager)
        {
            return new BannerDAC().GetNewsAdvRecordList(title, Version, ref pager);
        }

        public Banner GetById(int id)
        {
            return new BannerDAC().GetById(id);
        }

        public BannerFiles GetBannerFileByBannerId(int bannerId)
        {
            return new BannerDAC().GetBannerFileByBannerId(bannerId);
        }

        public SaveResult InsertBanner(Banner model, string[] bannerUrl)
        {
            var insertSuccess = false;
            if (CheckBannerTitleExist(model))
            {
                return new SaveResult(false, "This banner already exists!");
            }
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = new BannerDAC().InsertBanner(model);
                new BannerDAC().InsertBannerFiles(result, bannerUrl);

                scope.Complete();
            }
            insertSuccess = true;
            return new SaveResult(insertSuccess);
        }

        public SaveResult UpdateBanner(Banner model, string[] bannerUrl)
        {
            bool saveSuccess = false;
            if (CheckBannerTitleExist(model))
            {
                return new SaveResult(false, "This banner already exists!");
            }
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                new BannerDAC().UpdateBanner(model);
                new BannerDAC().DeleteBannerFileByBannerId(model.Id);
                new BannerDAC().InsertBannerFiles(model.Id, bannerUrl);

                scope.Complete();
            }
            saveSuccess = true;
            return new SaveResult(saveSuccess);
        }

        public bool CheckBannerTitleExist(Banner banner)
        {
            Banner model = new BannerDAC().CheckTitleExist(banner);

            return model != null;
        }

        public SaveResult Unpublished(int id)
        {
            var result = new BannerDAC().Unpublished(id);
            return new SaveResult(result);
        }

        public SaveResult Published(int id)
        {
            var result = new BannerDAC().Published(id);
            return new SaveResult(result);
        }

        public SaveResult DeleteById(int id)
        {
            var result = false;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                new BannerDAC().DeleteBannerById(id);
                new BannerDAC().DeleteBannerFileByBannerId(id);

                scope.Complete();
                result = true;
            }

            return new SaveResult(result);
        }

        public List<BannerListOM> GetBannerListByType(int pageSize, int pageIndex, int type, int version)
        {
            var list = new BannerDAC().GetBannerListByType(pageSize, pageIndex, type, version);

            return list.ToList();
        }

        public BannerOM CheckBanner(int id)
        {
            var banner = new BannerDAC().GetCheckById(id);
            if (banner == null)
                throw new ApplicationException();

            return banner;
        }

    }
}
