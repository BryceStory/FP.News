using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace FiiiPay.Enterprise.Web.Utils
{
    public static class XMLHelper
    {
        public static XElement GetRootNode(string XmlPath)
        {
            XElement xEle = XElement.Load(XmlPath);
            return xEle;
        }
        public static List<XElement> GetChildren(this XElement xEle, string NodeName)
        {
            if (xEle == null)
                return null;
            var xeList = xEle.Elements(NodeName).ToList();
            return xeList;
        }
    }
}