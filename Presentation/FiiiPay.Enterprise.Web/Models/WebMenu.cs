using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Models
{
    public class WebMenu
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string CountrollerName { get; set; }
        public string ActionName { get; set; }
        public string Path { get; set; }
        public bool HasChild { get; set; }
    }
}