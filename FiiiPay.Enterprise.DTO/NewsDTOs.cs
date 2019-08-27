using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.DTO
{
    public class NewsListOM
    {
        public int Id { get; set; }
        /// <summary>
        /// 中文标题
        /// </summary>
        public string CNTitle { get; set; }
        /// <summary>
        /// 英文标题
        /// </summary>
        public string ENTitle { get; set; }
        /// <summary>
        /// 中文正文
        /// </summary>
        public string CNContent { get; set; }
        /// <summary>
        /// 中文来源
        /// </summary>
        public string CNSource { get; set; }
        /// <summary>
        /// 英文版来源
        /// </summary>
        public string ENSource { get; set; }
        /// <summary>
        /// 英文正文
        /// </summary>
        public string ENContent { get; set; }
        /// <summary>
        /// 中文关键字
        /// </summary>
        public string CNKeyword1 { get; set; }
        public string CNKeyword2 { get; set; }
        /// <summary>
        /// 英文关键字
        /// </summary>
        public string ENKeyword1 { get; set; }
        public string ENKeyword2 { get; set; }
        /// <summary>
        /// 中文版封面图片
        /// </summary>
        public string CNCoverPictureUrl { get; set; }
        /// <summary>
        /// 英文版封面图片
        /// </summary>
        public string ENCoverPictureUrl { get; set; }
        /// <summary>
        /// 状态;已保存，展示中，已下架
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
       // public string ModifyTime { get; set; }
        /// <summary>
        /// 所属专栏
        /// </summary>
        public int NewsSetionId { get; set; }

        public int CNPageView { get; set; }
        public int ENPageView { get; set; }

        /// <summary>
        /// 中文版发布者
        /// </summary>
        public string CNPublisher { get; set; }
        /// <summary>
        /// 英文版发布者
        /// </summary>
        public string ENPublisher { get; set; }
        /// <summary>
        /// 中文简介
        /// </summary>
        public string CNIntro { get; set; }
        /// <summary>
        /// 英文简介
        /// </summary>
        public string ENIntro { get; set; }

    }

    public class NewsSectionOM
    {
        public int Id { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string CNName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string ENName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }

    public class NewsOM
    {
        public int Id { get; set; }
        /// <summary>
        /// 中文标题
        /// </summary>
        public string CNTitle { get; set; }
        /// <summary>
        /// 英文标题
        /// </summary>
        public string ENTitle { get; set; }
        /// <summary>
        /// 中文正文
        /// </summary>
        public string CNContent { get; set; }
        /// <summary>
        /// 中文来源
        /// </summary>
        public string CNSource { get; set; }
        /// <summary>
        /// 英文版来源
        /// </summary>
        public string ENSource { get; set; }
        /// <summary>
        /// 英文正文
        /// </summary>
        public string ENContent { get; set; }
        /// <summary>
        /// 中文关键字
        /// </summary>
        public string CNKeyword1 { get; set; }
        public string CNKeyword2 { get; set; }
        /// <summary>
        /// 英文关键字
        /// </summary>
        public string ENKeyword1 { get; set; }
        public string ENKeyword2 { get; set; }
        /// <summary>
        /// 中文版封面图片
        /// </summary>
        public string CNCoverPictureUrl { get; set; }
        /// <summary>
        /// 英文版封面图片
        /// </summary>
        public string ENCoverPictureUrl { get; set; }
        /// <summary>
        /// 状态;已保存，展示中，已下架
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        // public string ModifyTime { get; set; }
        public int CNPageView { get; set; }
        public int ENPageView { get; set; }
        /// <summary>
        /// 中文版发布者
        /// </summary>
        public string CNPublisher { get; set; }
        /// <summary>
        /// 英文版发布者
        /// </summary>
        public string ENPublisher { get; set; }
        /// <summary>
        /// 中文简介
        /// </summary>
        public string CNIntro { get; set; }
        /// <summary>
        /// 英文简介
        /// </summary>
        public string ENIntro { get; set; }
    }

    public class PageViewOM
    {
        public int CNPageView { get; set; }
        public int ENPageView { get; set; }
    }

}
