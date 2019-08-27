using FiiiPay.Enterprise.Data;
using FiiiPay.Enterprise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Business
{
   public class TradingRecordComponent
    {
        public List<Order> GetRecordList(string orderno, string username, byte? status, ref Tuple<string, string, int, int, int, int> pager)
        {
            return new TradingRecordDAC().GetPagedList(orderno, username, status,ref pager);
        }
    }
}
