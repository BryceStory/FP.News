using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Entities
{
   public class WhiteList
    {
        public long Id { get; set; }

        public Guid AccountId { get; set; }

        public string IP { get; set; }
    }
}
