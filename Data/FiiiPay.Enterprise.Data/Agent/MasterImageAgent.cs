using FiiiPay.Framework;
using Microsoft.Exchange.WebServices.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Data.Agent
{
    public class MasterImageAgent
    {
      //  private readonly ILog _log = LogManager.GetLogger(typeof(MasterImageAgent));
        private readonly string blob_URL = ConfigurationManager.AppSettings.Get("Foundation_URL");
        private static readonly Dictionary<string, byte[]> _images = new Dictionary<string, byte[]>();
        public MasterImageAgent() { }

        public byte[] Download(string id)
        {
            if (string.IsNullOrEmpty(blob_URL)) throw new ArgumentException("Blob_URL not found");

            if (_images.ContainsKey(id)) return _images[id];

            var url = blob_URL.TrimEnd('/') + "/File/Download?id=" + id;
            var token = GenerateToken();

            try
            {
                var resultStr = RestUtilities.GetJson(url, new Dictionary<string, string> { { "Authorization", "bearer " + token } });

                if (string.IsNullOrWhiteSpace(resultStr))
                {
                   // _log.ErrorFormat("Download master image error. url = {0},id = {1}", url, id);
                    return null;
                }
                var responseJson = JsonConvert.DeserializeObject<ServiceResult<object>>(resultStr);
                if (responseJson.Code != 0)
                {
                    throw new ApplicationException(responseJson.Message);
                }
                var b = Convert.FromBase64String(responseJson.Data.ToString());

                if (!_images.ContainsKey(id))
                {
                    _images.Add(id, b);
                }
                return b;
            }
            catch (Exception exception)
            {
                //_log.Error(exception);
            }

            return null;
        }

        public string Upload(string fileName, byte[] bytes)
        {
            if (string.IsNullOrEmpty(blob_URL)) throw new ArgumentException("Blob_URL not found");

            var url = blob_URL.TrimEnd('/') + "/File/Upload";
            var token = GenerateToken();

            var json = JsonConvert.SerializeObject(new
            {
                FileName = fileName,
                File = bytes,
                FileType = "img"
            });
            var result = RestUtilities.PostJson(url, new Dictionary<string, string> { { "Authorization", "bearer " + token } }, json);
            var data = JsonConvert.DeserializeObject<ServiceResult<object>>(result);
            return data.Code == 0 ? data.Data.ToString() : null;
        }

        private static string GenerateToken()
        {
            var clientKey = ConfigurationManager.AppSettings.Get("Foundation_ClientKey");
            var secret = ConfigurationManager.AppSettings.Get("Foundation_SecretKey");

            if (string.IsNullOrWhiteSpace(clientKey)) throw new ArgumentException(nameof(clientKey));
            if (string.IsNullOrWhiteSpace(secret)) throw new ArgumentException(nameof(secret));

            return GenerateToken(secret, clientKey);
        }

        private static string GenerateToken(string secretKey, string clientKey)
        {
            var password = DateTime.UtcNow.ToString("yyyyMMddHHmmss") + clientKey;
            var token = AES128.Encrypt(password, secretKey);

            return token;
        }
    }
}
