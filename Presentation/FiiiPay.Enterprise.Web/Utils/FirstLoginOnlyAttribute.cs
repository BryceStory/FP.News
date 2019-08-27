using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public class FirstLoginOnlyAttribute : Attribute
    {
        public FirstLoginOnlyAttribute() { }
    }
}