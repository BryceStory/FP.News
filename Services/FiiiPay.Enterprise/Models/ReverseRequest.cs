using Newtonsoft.Json;

namespace FiiiPay.Enterprise.Models
{
    /// <summary>
    /// Class FiiiPay.Enterprise.API.Models.ReverseRequest
    /// </summary>
    /// <seealso cref="BaseRequest" />
    public class ReverseRequest : BaseRequest
    {
        /// <summary>
        /// Gets or sets the partner transaction identifier.
        /// </summary>
        /// <value>
        /// The partner transaction identifier.
        /// </value>
        [JsonProperty("partner_trans_id")]
        public string PartnerTransactionId { get; set; }
    }
}