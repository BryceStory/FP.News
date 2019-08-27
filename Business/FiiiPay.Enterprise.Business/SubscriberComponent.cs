using FiiiPay.Enterprise.Data;
using FiiiPay.Enterprise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Business
{
    public class SubscriberComponent
    {
        public List<Subscriber> GetRecordList(string email, ref Tuple<string, string, int, int, int, int> pager)
        {
            return new SubscriberDAC().GetPagedList(email, ref pager);
        }

        public bool Insert(string email)
        {
            return new SubscriberDAC().Insert(email);
        }


    }
}
