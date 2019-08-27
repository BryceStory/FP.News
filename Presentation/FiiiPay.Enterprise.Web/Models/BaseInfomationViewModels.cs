using FiiiPay.Enterprise.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Models
{
    public class BaseInfomationModel
    {
        [Display(Name = "MerchantIdField", ResourceType = typeof(MerchantIndex))]
        public string MerchantId { get; set; }
        [Display(Name = "EmailField", ResourceType = typeof(MerchantIndex))]
        public string Email { get; set; }
        [Display(Name = "PasswordField", ResourceType = typeof(MerchantIndex))]
        public string Password { get; set; }
    }
}