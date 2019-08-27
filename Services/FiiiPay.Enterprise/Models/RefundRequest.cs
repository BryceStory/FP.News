using Newtonsoft.Json;

namespace FiiiPay.Enterprise.Models
{
    /// <summary>
    /// Class FiiiPay.Enterprise.API.Models.RefundRequest
    /// </summary>
    /// <seealso cref="BaseRequest" />
    public class RefundRequest : BaseRequest
    {
        /// <summary>
        /// Gets or sets the notify URL.
        /// </summary>
        /// <value>
        /// The notify URL.
        /// </value>
        [JsonProperty("notify_url")]
        public string NotifyURL { get; set; }

        /// <summary>
        /// Gets or sets the partner transaction identifier.
        /// </summary>
        /// <value>
        /// The partner transaction identifier.
        /// </value>
        [JsonProperty("partner_trans_id")]
        public string PartnerTransactionId { get; set; }

        /// <summary>
        /// Gets or sets the partner refund identifier.
        /// </summary>
        /// <value>
        /// The partner refund identifier.
        /// </value>
        [JsonProperty("partner_refund_id")]
        public string PartnerRefundId { get; set; }

        /// <summary>
        /// Gets or sets the refund amount.
        /// </summary>
        /// <value>
        /// The refund amount.
        /// </value>
        [JsonProperty("refund_amount")]
        public decimal? RefundAmount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the refund reson.
        /// </summary>
        /// <value>
        /// The refund reson.
        /// </value>
        [JsonProperty("refund_reson")]
        public string RefundReson { get; set; }
    }
}