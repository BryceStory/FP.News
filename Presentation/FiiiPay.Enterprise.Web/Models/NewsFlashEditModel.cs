using FiiiPay.Enterprise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Models
{
    public class NewsFlashEditModel
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
        /// 英文正文
        /// </summary>
        public string ENContent { get; set; }
        /// <summary>
        /// 中文来源
        /// </summary>
        public string CNSource { get; set; }
        /// <summary>
        /// 英文版来源
        /// </summary>
        public string ENSource { get; set; }
        /// <summary>
        /// 状态;已保存，展示中，已下架
        /// </summary>
        public int Status { get; set; }
        public string[] CNCoverPictureUrlList { get; set; }
        public string[] ENCoverPictureUrlList { get; set; }
        public int Version { get; set; }
        /// <summary>
        /// 利好(中文)
        /// </summary>
        public int CNGood { get; set; }
        /// <summary>
        /// 利空（中文版）
        /// </summary>
        public int CNBad { get; set; }
        /// <summary>
        /// 利好(中文)
        /// </summary>
        public int ENGood { get; set; }
        /// <summary>
        /// 利空（中文版）
        /// </summary>
        public int ENBad { get; set; }

    }
}