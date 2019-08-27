using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Entities
{
    public class Banner
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid AccountId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图片指定链接地址
        /// </summary>
        public string LinkUrl { get; set; }
        /// <summary>
        /// 版本  1：中文版 2：英文版
        /// </summary>
        public BannerVersion Version { get; set; }
        /// <summary>
        /// 图片类型  0：首页页顶主Banner 1;Fiii生态页顶Banner 2:新闻小Banner
        /// </summary>
        public BannerType Type { get; set; }
        /// <summary>
        /// 图片发布状态  0：已下架  1;已上架
        /// </summary>
        public BannerStatus Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 排序序号  数字越小排列越前
        /// </summary>
        public int Sort { get; set; }
    }
    public enum BannerVersion
    {
        /// <summary>
        /// 中文版
        /// </summary>
        CN=1,
        /// <summary>
        /// 英文版
        /// </summary>
        EN=2
    }
    public enum BannerType
    {
        /// <summary>
        /// 首页页顶主Banner
        /// </summary>
        HomePageBanner = 0,
        /// <summary>
        /// Fiii生态页顶Banner
        /// </summary>
        FiiiEcologyBanner = 1,
        /// <summary>
        /// 新闻小Banner
        /// </summary>
        NewsBanner = 2
    }
    public enum BannerStatus
    {
        /// <summary>
        /// 已下架
        /// </summary>
        Unpublished=0,
       /// <summary>
       /// 已上架
       /// </summary>
        Published=1
    }
}
