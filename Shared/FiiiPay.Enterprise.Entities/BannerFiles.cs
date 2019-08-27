using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Entities
{
    public class BannerFiles
    {
        public int Id { get; set; }
        /// <summary>
        /// Banner信息Id
        /// </summary>
        public int BannerId { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string FileUrl { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
