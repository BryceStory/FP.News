using Newtonsoft.Json;

namespace FiiiPay.Enterprise.Models
{
    /// <summary>
    /// Class FiiiPay.Enterprise.API.Models.ReverseResponse
    /// </summary>
    /// <seealso cref="BaseResponse" />
    public class ReverseResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the partner transaction identifier.
        /// </summary>
        /// <value>
        /// The partner transaction identifier.
        /// </value>
        [JsonProperty("partner_trans_id")]
        public string PartnerTransactionId { get; set; }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        [JsonProperty("fiiipay_trans_id")]
        public string TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the reverse time.
        /// </summary>
        /// <value>
        /// The reverse time.
        /// </value>
        [JsonProperty("fiiipay_reverse_time")]
        public string ReverseTime { get; set; }
    }
}