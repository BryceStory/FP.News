using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Entities
{
    public class PriceInfoHistory
    {
        public int ID { get; set; }
        public int CryptoID { get; set; }
        public int CurrencyID { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
