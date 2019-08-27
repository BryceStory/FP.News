using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Entities
{
    public class NewsFlash
    {

        public int Id { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid AccountsId { get; set; }
        /// <summary>
        /// 中文标题
        /// </summary>
        public string CNTitle { get; set; }
        /// <summary>
        /// 英文标题
        /// </summary>
        public string ENTitle { get; set; }
        /// <summary>
        /// 中文版浏览量
        /// </summary>
        public int? CNPageView { get; set; }
        /// <summary>
        /// 英文版浏览量
        /// </summary>
        public int? ENPageView { get; set; }
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
        public NewsFlashStatus Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 利好(中文)
        /// </summary>
        public int CNGood { get; set; }
        /// <summary>
        /// 利空（中文版）
        /// </summary>
        public int CNBad { get; set; }
        /// <summary>
        /// 利好(英文)
        /// </summary>
        public int ENGood { get; set; }
        /// <summary>
        /// 利空（英文）
        /// </summary>
        public int ENBad { get; set; }

    }

    public enum NewsFlashStatus
    {
        /// <summary>
        /// 已保存
        /// </summary>
        Saved = 0,
        /// <summary>
        /// 已上架
        /// </summary>
        Published = 1,
        /// <summary>
        /// 已下架
        /// </summary>
        Unpublished = 2
    }
}
