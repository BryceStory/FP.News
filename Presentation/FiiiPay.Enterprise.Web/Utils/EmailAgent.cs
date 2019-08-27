using FiiiPay.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace FiiiPay.Enterprise.Web.Utils
{
    public class EmailAgent
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="recipients">收件人，支持多收件人，使用逗号分割</param>
        /// <param name="subject">标题</param>
        /// <param name="content">内容</param>
        /// <returns>是否成功</returns>
        public bool Send(string recipients, string subject, string content)
        {
            var model = new { Recipients = recipients, Subject = subject, Content = content, Merchant = "FiiiLab", SendLevel = 5 };
            string url = ConfigurationManager.AppSettings["FP_EMAIL_API__URL"];
            string emailToken = ConfigurationManager.AppSettings["Email_Token"];
            RestUtilities.PostJson($"{url}/api/Message/PostEmail", new Dictionary<string, string> { { "bearer", emailToken } }, JsonConvert.SerializeObject(model));
            return true;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="recipients">收件人，支持多收件人，使用逗号分割</param>
        /// <param name="subject">标题</param>
        /// <param name="content">内容</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SendAsync(string recipients, string subject, string content)
        {
            var model = new { Recipients = recipients, Subject = subject, Content = content, Merchant = "FiiiLab", SendLevel = 5 };
            string url = ConfigurationManager.AppSettings["FP_EMAIL_API__URL"];
            string emailToken = ConfigurationManager.AppSettings["Email_Token"];
            var result = await RestUtilities.PostJsonAsync($"{url}/api/Message/PostEmail", new Dictionary<string, string> { { "bearer", emailToken } }, JsonConvert.SerializeObject(model));
            //{"Data":null,"DataCount":0,"IsSuccess":false,"ReasonCode":10010,"Message":"验证失败"}
            return true;
        }
    }
}