using FiiiPay.Framework.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Utils
{
    /// <summary>
    /// 安全验证入口
    /// </summary>
    public class SecurityVerify
    {
        public static string SendCode<T>(string uniqueKey, string target) where T : SecurityVerification, new()
        {
            var verifier = new T();
            var verifierName = typeof(T).Name;
            var timeInterval = verifier.GetSendCodeTimeInterval();
            var timeIntervalKey = verifier.GetSendTimeIntervalKey(verifierName, uniqueKey);
            if (timeInterval > 0)
            {
                var codesent = RedisHelper.StringGet(Configs.RedisIndex_Web_Verify, timeIntervalKey);
                if (codesent == "1")
                    return null;
            }
            bool needLock = verifier.LockWhenVerifyFailed();
            if (needLock)
            {
                var errorCountKey = verifier.GetErrorCountKey(verifierName, uniqueKey);
                CheckErrorCount(verifier, errorCountKey);
            }
            var maxSendCount = verifier.GetSendCodeMaxCount();
            int sendCount = 0;
            if (maxSendCount > 0)
            {
                sendCount = CheckSendCount(verifier, verifier.GetSendCountKey(verifierName, uniqueKey));
            }
            var code = verifier.SendCode(target);
            if (maxSendCount > 0)
            {
                var sendCountKey = verifier.GetSendCountKey(verifierName, uniqueKey);
                sendCount++;
                var dateNow = DateTime.Now;
                var tomorrowStartTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day).AddDays(1);
                RedisHelper.StringSet(Configs.RedisIndex_Web_Verify, sendCountKey, sendCount.ToString(), tomorrowStartTime - dateNow);
            }
            var codeKey = verifier.GetSendCodeKey(verifierName, uniqueKey);
            RedisHelper.StringSet(Configs.RedisIndex_Web_Verify, codeKey, code, TimeSpan.FromMinutes(verifier.GetVerifyCodeExpired()));
            RedisHelper.StringSet(Configs.RedisIndex_Web_Verify, timeIntervalKey, "1", TimeSpan.FromMinutes(timeInterval));
            return code;
        }
        /// <summary>
        /// 验证方法
        /// </summary>
        /// <typeparam name="T">SecurityVerification子类</typeparam>
        /// <param name="uniqueKey">唯一标志，用于组合缓存key</param>
        /// <param name="compareFrom">原始字符串，如果在缓存中可以传null</param>
        /// <param name="code">待验证验证码</param>
        /// <param name="deleteCodeKey">验证成功后是否自动删除验证码</param>
        public static void Verify<T>(string uniqueKey, string compareFrom, string code, bool deleteCodeKey = true) where T : SecurityVerification, new()
        {
            var verifier = new T();
            var verifierName = typeof(T).Name;
            bool needLock = verifier.LockWhenVerifyFailed();
            string codeKey = "";
            bool deleteCode = false;
            if (string.IsNullOrEmpty(compareFrom))
            {
                codeKey = verifier.GetSendCodeKey(verifierName, uniqueKey);
                compareFrom = RedisHelper.StringGet(Configs.RedisIndex_Web_Verify, codeKey);
                if (!string.IsNullOrEmpty(codeKey))
                    deleteCode = true;
            }
            deleteCode = deleteCode && deleteCodeKey;
            if (needLock)
            {
                var errorCountKey = verifier.GetErrorCountKey(verifierName, uniqueKey);
                int errorCount = CheckErrorCount(verifier, errorCountKey);
                var result = verifier.Verify(compareFrom, code);
                if (result)
                {
                    if (errorCount > 0)
                        DeleteErrorCount(errorCountKey);
                    if (deleteCode)
                    {
                        RedisHelper.KeyDelete(Configs.RedisIndex_Web_Verify, codeKey);
                    }
                }
                else
                {
                    IncreaseErrorCount(verifier, errorCountKey);
                }
            }
            else
            {
                var result = verifier.Verify(compareFrom, code);
                if (!result)
                    verifier.VerifyFaild(0);
                if (deleteCode)
                {
                    RedisHelper.KeyDelete(Configs.RedisIndex_Web_Verify, codeKey);
                }
            }
        }

        public static void DeleteCodeKey<T>(string uniqueKey) where T : SecurityVerification, new()
        {
            var verifier = new T();
            var verifierName = typeof(T).Name;
            var codeKey = verifier.GetSendCodeKey(verifierName, uniqueKey);
            RedisHelper.KeyDelete(Configs.RedisIndex_Web_Verify, codeKey);
        }

        private static int CheckSendCount(SecurityVerification verifier, string sendCountKey)
        {
            int.TryParse(RedisHelper.StringGet(Configs.RedisIndex_Web_Verify, sendCountKey), out int sendCount);

            if (sendCount >= verifier.GetSendCodeMaxCount())
            {
                verifier.SendCodeMoreTimes();
            }
            return sendCount;
        }

        private static int CheckErrorCount(SecurityVerification verifier, string errorCountKey)
        {
            int.TryParse(RedisHelper.StringGet(Configs.RedisIndex_Web_Verify, errorCountKey), out int errorCount);

            if (errorCount >= verifier.GetVerifyFailedMaxCount())
            {
                var minCount = GetErrorLockTime(errorCountKey);
                verifier.FailedMoreTimes(minCount);
            }
            return errorCount;
        }
        private static int GetErrorLockTime(string errorKey)
        {
            var ts = RedisHelper.KeyTimeToLive(Configs.RedisIndex_Web_Verify, errorKey);
            var minCount = ts.HasValue ? Math.Ceiling(ts.Value.TotalMinutes) : 1;
            return Convert.ToInt32(minCount);
        }
        private static void DeleteErrorCount(string errorCountKey)
        {
            RedisHelper.KeyDelete(Configs.RedisIndex_Web_Verify, errorCountKey);
        }
        private static void IncreaseErrorCount(SecurityVerification verifier, string errorCountKey)
        {
            var errorCountsStr = RedisHelper.StringGet(Configs.RedisIndex_Web_Verify, errorCountKey);
            int.TryParse(errorCountsStr, out int errorCount);
            ++errorCount;
            RedisHelper.StringSet(Configs.RedisIndex_Web_Verify, errorCountKey, errorCount.ToString(), TimeSpan.FromMinutes(verifier.GetVerifyFailedLockTime()));
            if (errorCount >= verifier.GetVerifyFailedMaxCount())
            {
                var minCount = GetErrorLockTime(errorCountKey);
                verifier.FailedMoreTimes(minCount);
            }
            else
            {
                verifier.VerifyFaild(verifier.GetVerifyFailedMaxCount() - errorCount);
            }
        }
    }
}