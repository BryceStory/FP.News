using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Models
{
    public class SubscriberViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}