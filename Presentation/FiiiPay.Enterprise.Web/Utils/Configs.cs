using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web
{
    /// <summary>
    /// 全局配置
    /// </summary>
    public class Configs
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public const string PlateForm = "FiiiEnterpriseWeb";
        /// <summary>
        /// 登陆cookie key
        /// </summary>
        public const string CookieKey_LoginInfo = "_LoginInfo";

        /// <summary>
        /// 默认缓存位置
        /// </summary>
        public const int RedisIndex_Web = 0;
        /// <summary>
        /// 验证数据缓存位置
        /// </summary>
        public const int RedisIndex_Web_Verify = 3;

        /// <summary>
        /// 验证码有效时间
        /// </summary>
        public const int VerifyCodeExpired = 5;
        /// <summary>
        /// 默认失败锁定时间内验证失败最大次数
        /// </summary>
        public const int VerifyFaildMaxCount = 5;
        /// <summary>
        /// 默认验证失败锁定时间
        /// </summary>
        public const int VerifyFaildLockTime = 60;
        /// <summary>
        /// 一天内发送验证码最大次数
        /// </summary>
        public const int SendCodeMaxCount = 25;
        /// <summary>
        /// 验证码发送时间间隔
        /// </summary>
        public const int SendCodeTimeInterval = 1;
    }
}