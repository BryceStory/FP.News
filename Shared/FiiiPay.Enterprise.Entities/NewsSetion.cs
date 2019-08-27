using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Entities
{
    public class NewsSetion
    {
        public int Id { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid AccountsId { get; set; }
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
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
