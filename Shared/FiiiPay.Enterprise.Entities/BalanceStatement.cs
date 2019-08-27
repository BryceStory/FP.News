using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Entities
{
   public class BalanceStatement
    {
        public long Id { get; set; }

        public Guid AccountId { get; set; }

        public string Action { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public decimal Timestamp { get; set; }

        public string Remark { get; set; }
    }
}
