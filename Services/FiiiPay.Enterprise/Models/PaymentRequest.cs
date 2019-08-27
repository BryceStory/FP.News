using Newtonsoft.Json;

namespace FiiiPay.Enterprise.Models
{
    /// <summary>
    /// Class FiiiPay.Enterprise.API.Models.PaymentRequest
    /// </summary>
    /// <seealso cref="BaseRequest" />
    public class PaymentRequest : BaseRequest
    {
        /// <summary>
        /// Gets or sets the seller identifier.
        /// </summary>
        /// <value>
        /// The seller identifier.
        /// </value>
        [JsonProperty("fiiipay_seller_id")]
        public string SellerId { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the name of the transaction.
        /// </summary>
        /// <value>
        /// The name of the transaction.
        /// </value>
        [JsonProperty("trans_name")]
        public string TransactionName { get; set; }

        /// <summary>
        /// Gets or sets the partner transaction identifier.
        /// </summary>
        /// <value>
        /// The partner transaction identifier.
        /// </value>
        [JsonProperty("partner_trans_id")]
        public string PartnerTransactionId { get; set; }

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
        public decimal TransactionAmount { get; set; }

        /// <summary>
        /// Gets or sets the user identity code.
        /// </summary>
        /// <value>
        /// The user identity code.
        /// </value>
        [JsonProperty("user_identity_code")]
        public string UserIdentityCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the identity code.
        /// </summary>
        /// <value>
        /// The type of the identity code.
        /// </value>
        [JsonProperty("identity_code_type")]
        public string IdentityCodeType { get; set; }

        /// <summary>
        /// Gets or sets the transaction crated time.
        /// </summary>
        /// <value>
        /// The transaction crated time.
        /// </value>
        [JsonProperty("trans_create_time")]
        public string TransactionCratedTime { get; set; }

        /// <summary>
        /// Gets or sets the memo.
        /// </summary>
        /// <value>
        /// The memo.
        /// </value>
        [JsonProperty("memo")]
        public string Memo { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        [JsonProperty("biz_product")]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the extend information.
        /// </summary>
        /// <value>
        /// The extend information.
        /// </value>
        [JsonProperty("extend_info")]
        public string ExtendInfo { get; set; }
    }
}