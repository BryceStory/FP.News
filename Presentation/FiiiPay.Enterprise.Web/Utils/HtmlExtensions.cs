using System;
using System.Resources;
using System.Web.Mvc;

namespace FiiiPay.Enterprise.Web
{
    public static class HtmlExtensions
    {
        public static string Resource(this HtmlHelper helper, string resourceFileName, string resourceKey)
        {
            try
            {
                var baseName = "FiiiPay.Enterprise.Web.Resources." + resourceFileName;
                ResourceManager rm = new ResourceManager(baseName, typeof(Resources.GeneralResource).Assembly);
                return rm.GetString(resourceKey);
            }
            catch
            {
                return resourceKey;
            }
        }
    }
}