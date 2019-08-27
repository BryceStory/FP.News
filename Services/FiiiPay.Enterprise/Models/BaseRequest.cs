using Newtonsoft.Json;

namespace FiiiPay.Enterprise.Models
{
    /// <summary>
    /// Class FiiiPay.Enterprise.API.Models.BaseRequest
    /// </summary>
    public abstract class BaseRequest
    {
        /// <summary>
        /// Gets or sets the sign.
        /// </summary>
        /// <value>
        /// The sign.
        /// </value>
        [JsonProperty("sign")]
        public string Sign { get; set; }

        /// <summary>
        /// Gets or sets the type of the sign.
        /// </summary>
        /// <value>
        /// The type of the sign.
        /// </value>
        [JsonProperty("sing_type")]
        public string SignType { get; set; }

        /// <summary>
        /// Gets or sets the partner identifier.
        /// </summary>
        /// <value>
        /// The partner identifier.
        /// </value>
        [JsonProperty("partner")]
        public string PartnerId { get; set; }

        /// <summary>
        /// Gets or sets the input character set.
        /// </summary>
        /// <value>
        /// The input character set.
        /// </value>
        [JsonProperty("_input_char_set")]
        public string InputCharSet { get; set; }
    }
}