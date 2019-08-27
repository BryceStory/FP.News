using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Entities
{
    public class NewsFlashFiles
    {
        public int Id { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int NewsFlashId { get; set; }

        /// <summary>
        /// 封面图片路径
        /// </summary>
        public string CoverPictureUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 图片所属快讯版本（0：中文版  1：英文版）
        /// </summary>
        public FlashVersion Version { get; set; }
    }

    public enum FlashVersion
    {
        /// <summary>
        ///中文版 
        /// </summary>
        CN = 0,
        /// <summary>
        /// 英文版
        /// </summary>
        EN = 1
    }
}
