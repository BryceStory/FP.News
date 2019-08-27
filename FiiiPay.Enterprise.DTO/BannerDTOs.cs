using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.DTO
{
    public class BannerOM
    {
        public int Id { get; set; }
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
        public int Version { get; set; }
        /// <summary>
        /// 图片类型  0：首页页顶主Banner 1;Fiii生态页顶Banner 2:新闻小Banner
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 图片发布状态  0：已下架  1;已上架
        /// </summary>
        public int Status { get; set; }
        public string CreateTime { get; set; }
        /// <summary>
        /// 排序序号  数字越小排列越前
        /// </summary>
        public int Sort { get; set; }
        public string FileUrl { get; set; }
    }

    public class BannerListOM
    {
        public int Id { get; set; }
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
        public int Version { get; set; }
        /// <summary>
        /// 图片类型  0：首页页顶主Banner 1;Fiii生态页顶Banner 2:新闻小Banner
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 图片发布状态  0：已下架  1;已上架
        /// </summary>
        public int Status { get; set; }
        public string CreateTime { get; set; }
        /// <summary>
        /// 排序序号  数字越小排列越前
        /// </summary>
        public int Sort { get; set; }
        public string FileUrl { get; set; }

    }
}
