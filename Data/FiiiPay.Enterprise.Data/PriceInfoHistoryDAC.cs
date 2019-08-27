using Dapper;
using FiiiPay.Enterprise.DTO;
using FiiiPay.Enterprise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Data
{
    public class PriceInfoHistoryDAC : BaseDataAccess
    {

        public IEnumerable<PriceInfoHistoryOM> GetPriceList(string cryptoList, string fiatcurrency)
        {
            string sql = @"select Symbol,id as CryptoID,
                         (select top 1 t1.Price from PriceInfo t1 where t1.CryptoID=t.ID and t1.CurrencyID=(select t2.ID from dbo.Fiatcurrencies t2 where t2.Code=@Fiatcurrency ) order by t1.LastUpdateDate desc) as Price
                         from Cryptocurrencies t  where t.Symbol in @CryptoList";

            using (var con = ReadConnectionToFiiiRates())
            {
                return con.Query<PriceInfoHistoryOM>(sql, new { CryptoList = cryptoList.Split(new string[] { "," },StringSplitOptions.RemoveEmptyEntries), Fiatcurrency = fiatcurrency });
            }
        }

    }
}
