using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Models
{
    public class OrderIndexModel
    {
        public Guid Id { get; set; }

        public string OrderNo { get; set; }

        public string MerchantOrderNo { get; set; }

        public byte Status { get; set; }

        public Guid AccountId { get; set; }

        public Guid UserAccountId { get; set; }

        public string UserName { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public decimal ExpiredTime { get; set; }

        public decimal TransactionFee { get; set; }

        public DateTime PaymentTime { get; set; }

        public string SourceData { get; set; }

        public bool Notification { get; set; }

        //public DateTime Timestamp { get; set; }
        public string Timestamp { get; set; }


        public string Remark { get; set; }
    }
}