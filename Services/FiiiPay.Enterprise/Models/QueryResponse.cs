using Newtonsoft.Json;

namespace FiiiPay.Enterprise.Models
{
    /// <summary>
    /// Class FiiiPay.Enterprise.API.Models.QueryResponse
    /// </summary>
    /// <seealso cref="BaseResponse" />
    public class QueryResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the transaction status.
        /// </summary>
        /// <value>
        /// The transaction status.
        /// </value>
        [JsonProperty("fiiipay_trans_status")]
        public string TransactionStatus { get; set; }

        /// <summary>
        /// Gets or sets the buyer login identifier.
        /// </summary>
        /// <value>
        /// The buyer login identifier.
        /// </value>
        [JsonProperty("fiiipay_user_login_id ")]
        public string BuyerLoginId { get; set; }

        /// <summary>
        /// Gets or sets the buyer user identifier.
        /// </summary>
        /// <value>
        /// The buyer user identifier.
        /// </value>
        [JsonProperty("fiiipay_user_user_id ")]
        public string BuyerUserId { get; set; }

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
        /// Gets or sets the pay time.
        /// </summary>
        /// <value>
        /// The pay time.
        /// </value>
        [JsonProperty("fiiipay_pay_time ")]
        public string PayTime { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the transaction amount.
        /// </summary>
        /// <value>
        /// The transaction amount.
        /// </value>
        [JsonProperty("trans_amount")]
        public decimal? TransactionAmount { get; set; }

        /// <summary>
        /// Gets or sets the exchange rate.
        /// </summary>
        /// <value>
        /// The exchange rate.
        /// </value>
        [JsonProperty("exchange_rate")]
        public decimal? ExchangeRate { get; set; }

        //[JsonProperty("trans_amount_cny")]
        //public decimal TransactionAmountCNY { get; set; }
    }
}