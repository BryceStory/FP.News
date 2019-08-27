using System.Web.Http;

namespace FiiiPay.Enterprise.API
{
    /// <summary>
    /// Class FiiiPay.Enterprise.API.WebApiApplication
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Applications the start.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
