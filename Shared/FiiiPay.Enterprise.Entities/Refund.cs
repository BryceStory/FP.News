using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Entities
{
   public class Refund
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public DateTime Timestamp { get; set; }

        public byte Status { get; set; }

        public bool Notification { get; set; }

        public string SourceData { get; set; }

        public string Remark { get; set; }
    }
}
