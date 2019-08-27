using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.DTO
{
    public class PriceInfoHistoryOM
    {
        public string Symbol { get; set; }
        public int CryptoID { get; set; }
        public decimal Price { get; set; }      
    }
}
