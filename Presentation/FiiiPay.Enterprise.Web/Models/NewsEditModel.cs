using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Models
{
    public class NewsEditModel
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
        public string[] CNCoverPictureUrlList { get; set; }
        public string[] ENCoverPictureUrlList { get; set; }
        /// <summary>
        /// 状态;已保存，展示中，已下架
        /// </summary>
        public int Status { get; set; }
        public int Version { get; set; }
        public int NewsSetionId { get; set; }
        /// <summary>
        /// 中文版浏览量
        /// </summary>
        public int CNPageView { get; set; }
        /// <summary>
        /// 英文版浏览量
        /// </summary>
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
}