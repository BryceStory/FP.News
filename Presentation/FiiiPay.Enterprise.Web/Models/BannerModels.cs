using FiiiPay.Enterprise.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Models
{
    public class BannerModel : PageModel
    {
        [Required]
        public int Type { get; set; }

        [Required]
        public int Version { get; set; }
    }

    public class BannerDetailModel
    {
        /// <summary>
        /// Banner Id
        /// </summary>
        public int Id { get; set; }
    }
}