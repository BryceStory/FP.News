using FiiiPay.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Filters;

namespace FiiiPay.Enterprise.Web.filters
{
    public class ApiExceptionFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;

            if (exception is ApplicationException)
            {
                var resultData = new ServiceResult<string>
                {
                    Code = 100036,
                    Message=exception.Message
                };
                actionExecutedContext.Response = new System.Net.Http.HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<ServiceResult<string>>(resultData,new JsonMediaTypeFormatter())
                };
            }
        }
    }
}