using FiiiPay.Enterprise.Data;
using FiiiPay.Enterprise.DTO;
using FiiiPay.Enterprise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Business
{
    public class PriceInfoHistoryComponent
    {
        public List<PriceInfoHistoryOM> GetPriceList(string cryptoList,string fiatcurrency)
        {
            var list = new PriceInfoHistoryDAC().GetPriceList(cryptoList,fiatcurrency).ToList();

            return list.Select(t => new PriceInfoHistoryOM
            {
                CryptoID=t.CryptoID,
                Symbol=t.Symbol,
                Price=t.Price,
            }).ToList();
        }
    }
}
