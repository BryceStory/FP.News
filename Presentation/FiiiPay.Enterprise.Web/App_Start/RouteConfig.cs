using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FiiiPay.Enterprise.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute("");


            string language = Utils.CultureHelper.GetLanguage();

            routes.Add(new Route("", new RouteValueDictionary(new { lang = language, controller = "Home", action = "Index" }), new MultilLangRouteHandler()));
            routes.Add(new Route("login", new RouteValueDictionary(new { lang = language, controller = "Account", action = "Login" }), new MultilLangRouteHandler()));
            routes.Add(new Route("{lang}/firstsetting", new RouteValueDictionary(new { lang = language, controller = "Account", action = "FirstSetting" }), new MultilLangRouteHandler()));
            routes.Add(new Route("{lang}/login", new RouteValueDictionary(new { lang = language, controller = "Account", action = "Login" }), new MultilLangRouteHandler()));
            routes.Add(new Route("{lang}/fbpsw", new RouteValueDictionary(new { lang = language, controller = "Account", action = "FbPsw" }), new MultilLangRouteHandler()));
            routes.Add(new Route("{lang}/{controller}/{action}/{id}", new RouteValueDictionary(new { lang = language, controller = "Home", action = "Index", id = UrlParameter.Optional }), new MultilLangRouteHandler()));

            routes.MapRoute(name: "home", url: "", defaults: new { lang = language, controller = "Home", action = "Index" });
            routes.MapRoute(name: "login", url: "login", defaults: new { lang = language, controller = "Account", action = "Login" });
            routes.MapRoute(name: "culturelogin", url: "{lang}/login", defaults: new { lang = language, controller = "Account", action = "Login" });
            routes.MapRoute(name: "fbpsw", url: "{lang}/fbpsw", defaults: new { lang = language, controller = "Account", action = "FbPsw" });
            routes.MapRoute(name: "firstsetting", url: "{lang}/firstsetting", defaults: new { lang = language, controller = "Account", action = "FirstSetting" });
            routes.MapRoute(
                name: "Default",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { lang = language, controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
