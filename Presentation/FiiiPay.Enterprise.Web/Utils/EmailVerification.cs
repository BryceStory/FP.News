using FiiiPay.Enterprise.Web.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Utils
{
    /// <summary>
    /// Email验证类，如需区分业务，则应添加子类，重写GetBussniess()
    /// 错误次数全局共享，发送次数全局共享
    /// </summary>
    public class EmailVerification : SecurityVerification
    {
        public override string SendCode(string targetEmail)
        {
            //return Framework.RandomAlphaNumericGenerator.GenerateAllNumber(6);
            return "123456";
        }
        public override bool Verify(string compareFrom, string code)
        {
            try
            {
                return compareFrom.Equals(code, StringComparison.InvariantCultureIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
        
        public override string GetSendCodeKey(string verificationName, string uniqueKey)
        {
            return $"{Configs.PlateForm}:EmailVerification:SendCode:{GetBussniess()}:{uniqueKey}";
        }
        public override string GetSendTimeIntervalKey(string verificationName, string uniqueKey)
        {
            return $"{Configs.PlateForm}:EmailVerification:SendTimeInterval:{GetBussniess()}:{uniqueKey}";
        }
        public override string GetSendCountKey(string verificationName, string uniqueKey)
        {
            return $"{Configs.PlateForm}:EmailVerification:SendCount:{uniqueKey}";
        }

        public override void FailedMoreTimes(int minCount)
        {
        }

        public override void VerifyFaild(int remainCount)
        {
            throw new ApplicationException(GeneralResource.VerifyCodeIncorrect);
        }
        public override void SendCodeMoreTimes()
        {
            throw new ApplicationException(GeneralResource.EmailCodeIncorrectMoreTimes);
        }

        public override bool LockWhenVerifyFailed()
        {
            return false;
        }
    }

    public class FirstSettingEmailVerification : EmailVerification
    {
        public override string GetBussniess()
        {
            return "FirstSettingEmail";
        }
    }
    public class FbPswEmailVerification : EmailVerification
    {
        public override string GetBussniess()
        {
            return "FbPswEmailVerification";
        }
    }
    public class ResetEmailOldVerification : EmailVerification
    {
        public override string GetBussniess()
        {
            return "ResetEmailOldVerification";
        }
    }

    public class ResetEmailOldTokenVerification : EmailVerification
    {
        public override string SendCode(string targetEmail)
        {
            return Framework.RandomAlphaNumericGenerator.Generate(8);
        }
        public override bool Verify(string compareFrom, string code)
        {
            var compareFrom1 = compareFrom + Encrypts.GenerateTicksInTenTime();
            var compareFrom2 = compareFrom + (Encrypts.GenerateTicksInTenTime() - 1);
            var result = PasswordHasher.VerifyHashedPassword(code, compareFrom1);
            result = result || PasswordHasher.VerifyHashedPassword(code, compareFrom2);
            return result;
        }
        public override string GetBussniess()
        {
            return "ResetEmailOldTokenVerification";
        }
        public override int GetVerifyCodeExpired()
        {
            return 10;
        }
    }
    public class ResetEmailNewVerification : EmailVerification
    {
        public override string GetBussniess()
        {
            return "ResetEmailNewVerification";
        }
    }
}