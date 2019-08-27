using FiiiPay.Enterprise.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Models
{
    public class OrderViewModel
    {
       
        [Required]
        [MaxLength(50)]
        public string OrderNo { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public byte Status { get; set; }
       
    }
}