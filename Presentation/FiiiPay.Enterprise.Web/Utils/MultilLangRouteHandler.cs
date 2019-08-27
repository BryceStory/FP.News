using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FiiiPay.Enterprise.Web
{
    public class MultilLangRouteHandler: MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var lang = requestContext.RouteData.Values["lang"];
            if (lang == null)
            {
                var userlang = requestContext.HttpContext.Request.UserLanguages;
                bool isZh = userlang != null && userlang.Length > 0 && userlang[0].StartsWith("zh-");
                lang = isZh ? "cn" : "en";
                requestContext.RouteData.Values["lang"] = lang;
            }
            string cultureString = lang.ToString() == "cn" ? "zh-cn" : "en-us";
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureString);
            return base.GetHttpHandler(requestContext);
        }
    }
}