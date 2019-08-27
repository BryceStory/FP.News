using Newtonsoft.Json;

namespace FiiiPay.Enterprise.Models
{
    /// <summary>
    /// Class FiiiPay.Enterprise.API.Models.RefundResponse
    /// </summary>
    /// <seealso cref="BaseResponse" />
    public class RefundResponse : BaseResponse
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
        /// Gets or sets the exchange rate.
        /// </summary>
        /// <value>
        /// The exchange rate.
        /// </value>
        [JsonProperty("exchange_rate")]
        public decimal? ExchangeRate { get; set; }

        //[JsonProperty("refund_amount_cny")]
        //public decimal RefundAmountCNY { get; set; }
    }
}