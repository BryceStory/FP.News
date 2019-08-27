using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Utils
{
    public class CultureHelper
    {
        public static string GetLanguage()
        {
            string language = "en";
            var cultureName = System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToLower();
            if (cultureName.StartsWith("zh-"))
                language = "cn";
            return language;
        }
    }
}