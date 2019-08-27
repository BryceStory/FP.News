using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Utils
{
    /// <summary>
    /// ajax请求返回内容
    /// </summary>
    public class ResponesResult
    {
        public ResponesResult(bool status)
        {
            Status = status;
        }

        public ResponesResult(bool status, string message)
        {
            Status = status;
            Message = message;
        }

        public bool Status { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return "{" + $"\"Status\":{(Status?1:0)},\"Message\":\"{Message}\"" + "}";
        }
    }
}