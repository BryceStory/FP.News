﻿using FiiiPay.Enterprise.Web.filters;
using System.Web;
using System.Web.Mvc;

namespace FiiiPay.Enterprise.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new EnterpriseErrorAttribute());
        }
    }
}
