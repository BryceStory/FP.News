using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Models
{
    public enum SignInStatus
    {
        Success = 0,
        LockedOut = 1,
        RequiresVerification = 2,
        Failure = 3
    }

    public enum NewsSubmitFormType
    {
        /// <summary>
        /// 中文版
        /// </summary>
        CNVersion = 0,
        /// <summary>
        /// 英文版
        /// </summary>
        ENVersion = 1
    }

    public enum NewsFlashSubmitFormType
    {
        /// <summary>
        /// 中文版
        /// </summary>
        CNVersion = 0,
        /// <summary>
        /// 英文版
        /// </summary>
        ENVersion = 1
    }

   
}