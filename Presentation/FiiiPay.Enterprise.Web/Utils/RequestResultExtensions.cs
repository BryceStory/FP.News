using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;

namespace FiiiPay.Enterprise.Web
{
    public static class RequestResultExtensions
    {
        public static string GetMessage(this Business.RequestResult result)
        {
            var baseName = "FiiiPay.Enterprise.Web.Resources." + result.MessageResFileName;
            ResourceManager rm = new ResourceManager(baseName, typeof(Resources.GeneralResource).Assembly);
            return rm.GetString(result.MessageResKey);
        }
        public static string GetMessage<T>(this Business.RequestResult<T> result)
        {
            var baseName = "FiiiPay.Enterprise.Web.Resources." + result.MessageResFileName;
            ResourceManager rm = new ResourceManager(baseName, typeof(Resources.GeneralResource).Assembly);
            return rm.GetString(result.MessageResKey);
        }
    }
}