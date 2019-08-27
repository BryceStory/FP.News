namespace FiiiPay.Enterprise.Models
{
    /// <summary>
    /// Class FiiiPay.Enterprise.API.Models.ModelExtension
    /// </summary>
    public static class ModelExtension
    {
        /// <summary>
        /// Successes the specified response.
        /// </summary>
        /// <param name="response">The response.</param>
        public static void Success(this BaseResponse response)
        {
            response.IsSuccess = "T";
            response.ResultCode = "SUCCESS";
        }
    }
}