using FiiiPay.Enterprise.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Models
{
    public class NewsFlashModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public byte Status { get; set; }
    }

    public class NewsFlashListModel: PageModel
    {

    }
}