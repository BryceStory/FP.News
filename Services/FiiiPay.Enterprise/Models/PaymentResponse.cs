using Newtonsoft.Json;

namespace FiiiPay.Enterprise.Models
{
    /// <summary>
    /// Class FiiiPay.Enterprise.API.Models.PaymentResponse
    /// </summary>
    /// <seealso cref="BaseResponse" />
    public class PaymentResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the user login identifier.
        /// </summary>
        /// <value>
        /// The user login identifier.
        /// </value>
        [JsonProperty("fiiipay_user_login_id ")]
        public string UserLoginId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [JsonProperty("fiiipay_user_id ")]
        public string UserId { get; set; }

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