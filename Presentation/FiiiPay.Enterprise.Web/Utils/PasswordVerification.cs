using FiiiPay.Enterprise.Web.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Utils
{
    /// <summary>
    /// 密码验证
    /// </summary>
    public class PasswordVerification : SecurityVerification
    {
        public override string SendCode(string target)
        {
            throw new NotImplementedException();
        }
        public override bool Verify(string compareFrom, string code)
        {
            try
            {
                return PasswordHasher.VerifyHashedPassword(compareFrom, code);
            }
            catch
            {
                return false;
            }
        }

        public override void FailedMoreTimes(int minCount)
        {
            throw new ApplicationException(string.Format(GeneralResource.PasswordIncorrectMoreTimes, minCount));
        }

        public override void VerifyFaild(int remainCount)
        {
            throw new ApplicationException(string.Format(GeneralResource.PasswordNotMatch, remainCount));
        }
        public override void SendCodeMoreTimes()
        {
            throw new NotImplementedException();
        }
    }
}