using FiiiPay.Enterprise.Models;
using Microsoft.AspNetCore.Mvc;

namespace FiiiPay.Enterprise.API.Controllers
{
    /// <summary>
    /// Class FiiiPay.Enterprise.API.Controllers.PaymentController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        /// <summary>
        /// Pays the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("pay")]
        [HttpPost]
        public PaymentResponse Pay(PaymentRequest request)
        {
            var response = new PaymentResponse();

            response.Success();

            return response;
        }

        /// <summary>
        /// Refunds the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("refund")]
        [HttpPost]
        public RefundResponse Refund(RefundRequest request)
        {
            var response = new RefundResponse();

            response.Success();

            return response;
        }

        /// <summary>
        /// Cancels the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("cancel")]
        [HttpPost]
        public CancelResponse Cancel(CancelRequest request)
        {
            var response = new CancelResponse();

            response.Success();

            return response;
        }

        /// <summary>
        /// Reverses the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("reverse")]
        [HttpPost]
        public ReverseResponse Reverse(ReverseRequest request)
        {
            var response = new ReverseResponse();

            response.Success();

            return response;
        }

        /// <summary>
        /// Queries the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Route("query")]
        [HttpGet]
        public QueryResponse Query(QueryRequest request)
        {
            var response = new QueryResponse();

            response.Success();

            return response;
        }
    }
}
