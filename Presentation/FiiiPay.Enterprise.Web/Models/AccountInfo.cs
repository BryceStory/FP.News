using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Models
{
    /// <summary>
    /// 账户信息
    /// </summary>
    public class AccountInfo
    {
        public Guid Id { get; set; }
        public byte Status { get; set; }
        public string Username { get; set; }
        public string MerchantName { get; set; }
        public string Token { get; set; }
    }
}