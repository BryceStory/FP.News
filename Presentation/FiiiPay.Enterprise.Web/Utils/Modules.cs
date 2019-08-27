using FiiiPay.Enterprise.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace FiiiPay.Enterprise.Web.Utils
{
    public class Modules
    {
        public static Dictionary<WebMenu, List<WebMenu>> GetModules()
        {
            var currentLanguage = HttpContext.Current.Request.RequestContext.RouteData.Values["lang"].ToString();
            //currentLanguage = "cn";  //911
            Dictionary<WebMenu, List<WebMenu>> menus = new Dictionary<WebMenu, List<WebMenu>>();
            string xmlPath = HttpContext.Current.Server.MapPath("~/Resources/MenuConfig.xml");
            var moduleList = XMLHelper.GetRootNode(xmlPath).GetChildren("Module");
            if (moduleList != null && moduleList.Count > 0)
            {
                List<XElement> moduleMenuList;
                List<WebMenu> menuItems;
                foreach (var rootMenu in moduleList)
                {
                    menuItems = new List<WebMenu>();
                    moduleMenuList = rootMenu.Elements("MenuItem").ToList();
                    var rootItem = TransferXmlToModule(rootMenu, moduleMenuList.Count > 0, currentLanguage);
                    foreach (var moduleMenu in moduleMenuList)
                    {
                        menuItems.Add(TransferXmlToModule(moduleMenu, false, currentLanguage));
                    }
                    menus.Add(rootItem, menuItems);
                }
            }
            return menus;
        }
        private static WebMenu TransferXmlToModule(XElement xmlNode, bool hasChild,string lang)
        {
            bool isZh = lang == "cn";
            return new WebMenu()
            {
                Name = isZh ? xmlNode.Attribute("namecn")?.Value : xmlNode.Attribute("name")?.Value,
                CountrollerName = xmlNode.Attribute("controllername")?.Value,
                ActionName = xmlNode.Attribute("actionname")?.Value,
                HasChild = hasChild,
                Icon = xmlNode.Attribute("icon")?.Value,
                Path = $"/{lang}/{xmlNode.Attribute("controllername")?.Value}/{xmlNode.Attribute("actionname")?.Value}"
            };
        }
    }
}