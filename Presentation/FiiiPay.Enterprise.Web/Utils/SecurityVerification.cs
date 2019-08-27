using FiiiPay.Enterprise.Web.Resources;
using FiiiPay.Framework;
using FiiiPay.Framework.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Utils
{
    /// <summary>
    /// 验证类基类
    /// </summary>
    public abstract class SecurityVerification
    {
        public abstract string SendCode(string target);
        public abstract bool Verify(string compareFrom, string Code);
        public abstract void FailedMoreTimes(int minCount);
        public abstract void VerifyFaild(int remainCount);
        public abstract void SendCodeMoreTimes();
        public virtual string GetErrorCountKey(string verificationName, string uniqueKey)
        {
            return $"{Configs.PlateForm}:{verificationName}:ErrorCounts:{uniqueKey}";
        }
        public virtual string GetSendCountKey(string verificationName, string uniqueKey)
        {
            return $"{Configs.PlateForm}:{verificationName}:SendCount:{uniqueKey}";
        }
        public virtual string GetSendCodeKey(string verificationName, string uniqueKey)
        {
            if(string.IsNullOrEmpty(GetBussniess()))
                return $"{Configs.PlateForm}:{verificationName}:SendCode:{uniqueKey}";
            else
                return $"{Configs.PlateForm}:{verificationName}:SendCode:{GetBussniess()}:{uniqueKey}";
        }
        public virtual string GetSendTimeIntervalKey(string verificationName, string uniqueKey)
        {
            if (string.IsNullOrEmpty(GetBussniess()))
                return $"{Configs.PlateForm}:{verificationName}:SendTimeInterval:{uniqueKey}";
            else
                return $"{Configs.PlateForm}:{verificationName}:SendTimeInterval:{GetBussniess()}:{uniqueKey}";
        }
        public virtual string GetBussniess()
        {
            return null;
        }
        public virtual int GetVerifyCodeExpired()
        {
            return Configs.VerifyCodeExpired;
        }
        public virtual int GetVerifyFailedMaxCount()
        {
            return Configs.VerifyFaildMaxCount;
        }
        public virtual int GetVerifyFailedLockTime()
        {
            return Configs.VerifyFaildLockTime;
        }
        /// <summary>
        /// 验证码发送时间间隔,小于或等于0表示可以无间隔发送
        /// </summary>
        /// <returns></returns>
        public virtual int GetSendCodeTimeInterval()
        {
            return Configs.SendCodeTimeInterval;
        }
        /// <summary>
        /// 一天类最多发送验证码次数，小于或等于0表示无限次发送且不会记录发送次数
        /// </summary>
        /// <returns></returns>
        public virtual int GetSendCodeMaxCount()
        {
            return Configs.SendCodeMaxCount;
        }
        /// <summary>
        /// 错误多次是否锁定，不锁定则不会记录错误次数
        /// </summary>
        /// <returns></returns>
        public virtual bool LockWhenVerifyFailed()
        {
            return true;
        }
    }
}