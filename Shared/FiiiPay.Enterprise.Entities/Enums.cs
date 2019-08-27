using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Entities
{
    /// <summary>
    /// 账户状态
    /// </summary>
    public enum AccountStatus
    {
        /// <summary>
        /// 已禁用
        /// </summary>
        Disabled,
        /// <summary>
        /// 已注册，未绑定邮箱与设置密码
        /// </summary>
        Registered,
        /// <summary>
        /// 正常状态
        /// </summary>
        Active
    }

    public enum NFGoodBadSelectStatus
    {
        /// <summary>
        /// 只点击一次利好
        /// </summary>
        GoodOnce = 1,
        /// <summary>
        ///点击第二次利好
        /// </summary>
        GoodTwice = 2,
        /// <summary>
        /// 点击第一次利空
        /// </summary>
        BadOnce = 3,
        /// <summary>
        /// 点击第二次利空
        /// </summary>
        BadTwice = 4,
        /// <summary>
        /// 已点击利好，再点击利空
        /// </summary>
        FirstGoodThenBad = 5,
        /// <summary>
        /// 已点击利空，再点击利好
        /// </summary>
        FirstBadThenGood = 6
    }

    public enum NewsVersionEnum
    {
        /// <summary>
        /// 中文版
        /// </summary>
        CNVersion = 0,
        /// <summary>
        /// 英文版
        /// </summary>
        ENVersion = 1
    }
}
