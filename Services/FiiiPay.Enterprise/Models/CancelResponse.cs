using Newtonsoft.Json;

namespace FiiiPay.Enterprise.Models
{
    /// <summary>
    /// Class FiiiPay.Enterprise.API.Models.CancelResponse
    /// </summary>
    /// <seealso cref="BaseResponse" />
    public class CancelResponse : BaseResponse
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
        /// Gets or sets the cancel time.
        /// </summary>
        /// <value>
        /// The cancel time.
        /// </value>
        [JsonProperty("fiiipay_cancel_time")]
        public string CancelTime { get; set; }
    }
}