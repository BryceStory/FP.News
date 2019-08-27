using FiiiPay.Enterprise.Web.Models.Common;
using FiiiPay.Enterprise.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FiiiPay.Enterprise.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceName = "StringLengthError", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "MerchantIDField", ResourceType =typeof(AccountLogin))]
        //[EmailAddress(ErrorMessageResourceName = "FieldFormatError", ErrorMessageResourceType = typeof(GeneralResource))]
        public string MerchantId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = "StringLengthError", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "PasswordField", ResourceType = typeof(AccountLogin))]
        public string Password { get; set; }
    }

    public class FindBackPasswordViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = "StringLengthError", ErrorMessageResourceType = typeof(GeneralResource))]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordField", ResourceType = typeof(AccountFirstSetting))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPasswordField", ResourceType = typeof(AccountFirstSetting))]
        [Compare("Password", ErrorMessageResourceName = "ConfirmPasswordNotMatch", ErrorMessageResourceType = typeof(AccountFirstSetting))]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress(ErrorMessageResourceName = "FieldFormatError", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "EmailField", ResourceType = typeof(AccountFirstSetting))]
        public string Email { get; set; }
        [Required]
        [Display(Name = "CodeField", ResourceType = typeof(AccountFirstSetting))]
        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessageResourceName = "StringLengthError", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "MerchantIDField", ResourceType = typeof(AccountLogin))]
        public string MerchantId { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = "StringLengthError", ErrorMessageResourceType = typeof(GeneralResource))]
        [DataType(DataType.Password)]
        [Display(Name = "OldPasswordField", ResourceType = typeof(AccountResetPsw))]
        public string OldPassword { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = "StringLengthError", ErrorMessageResourceType = typeof(GeneralResource))]
        [DataType(DataType.Password)]
        [Display(Name = "NewPasswordField", ResourceType = typeof(AccountResetPsw))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPasswordField", ResourceType = typeof(AccountResetPsw))]
        [Compare("NewPassword", ErrorMessageResourceName = "ConfirmPasswordNotMatch", ErrorMessageResourceType = typeof(AccountResetPsw))]
        public string ConfirmNewPassword { get; set; }
    }

    public class ResetEmailOldViewModel
    {
        [Required]
        [EmailAddress(ErrorMessageResourceName = "FieldFormatError", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "OriginalEmailField", ResourceType = typeof(AccountResetEmail))]
        public string Email { get; set; }

        [Required]
        [Display(Name = "CodeField", ResourceType = typeof(AccountResetEmail))]
        public string Code { get; set; }
    }
    public class ResetEmailNewViewModel
    {
        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessageResourceName = "StringLengthError", ErrorMessageResourceType = typeof(GeneralResource))]
        public string Token { get; set; }
        [Required]
        [EmailAddress(ErrorMessageResourceName = "FieldFormatError", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "NewEmailField", ResourceType = typeof(AccountResetEmail))]
        public string Email { get; set; }

        [Required]
        [Display(Name = "CodeField", ResourceType = typeof(AccountResetEmail))]
        public string Code { get; set; }
    }

    public class AccountSearchModel: GridPager
    {
        [Required]
        public string Username { get; set; }
    }

    public class AccountViewModel
    {
        public Guid Id { get; set; }

        public string Cellphone { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string MerchantName { get; set; }

        public string Password { get; set; }

        public string PIN { get; set; }

        public byte Status { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public int? RoleId { get; set; }
        public Guid CreateBy { get; set; }
        public Guid ModifyBy { get; set; }
    }
}
