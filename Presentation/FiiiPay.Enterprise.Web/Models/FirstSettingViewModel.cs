using FiiiPay.Enterprise.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Models
{
    public class FirstSettingViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 6,ErrorMessageResourceName = "StringLengthError", ErrorMessageResourceType = typeof(GeneralResource))]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordField", ResourceType = typeof(AccountFirstSetting))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPasswordField", ResourceType = typeof(AccountFirstSetting))]
        [Compare("Password",ErrorMessageResourceName = "ConfirmPasswordNotMatch",ErrorMessageResourceType = typeof(AccountFirstSetting))]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress(ErrorMessageResourceName = "FieldFormatError", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "EmailField", ResourceType = typeof(AccountFirstSetting))]
        public string Email { get; set; }
        [Required]
        [Display(Name = "CodeField", ResourceType = typeof(AccountFirstSetting))]
        public string Code { get; set; }
    }
}