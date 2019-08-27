using Newtonsoft.Json;

namespace FiiiPay.Enterprise.Models
{
    /// <summary>
    /// Class FiiiPay.Enterprise.API.Models.BaseResponse
    /// </summary>
    public abstract class BaseResponse
    {
        /// <summary>
        /// Gets or sets the is success.
        /// </summary>
        /// <value>
        /// The is success.
        /// </value>
        [JsonProperty("is_success")]
        public string IsSuccess { get; set; }

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
        /// Gets or sets the result code.
        /// </summary>
        /// <value>
        /// The result code.
        /// </value>
        [JsonProperty("result_code")]
        public string ResultCode { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        [JsonProperty("error")]
        public string ErrorCode { get; set; }
    }
}