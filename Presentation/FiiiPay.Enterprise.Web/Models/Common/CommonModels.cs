using FiiiPay.Framework.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FiiiPay.Enterprise.Web.Models.Common
{
    public class GetByIdModel<T> where T : struct
    {
        [Range(0, int.MaxValue)]
        public T Id { get; set; }
    }
    public class GetByGuidModel
    {
        [RequiredGuid]
        public Guid Id { get; set; }
    }
    public class GetByStringModel
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Id { get; set; }
    }
    public class GetByCodeModel
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Code { get; set; }
    }
    public class PageModel
    {
        private int? size;
        private int? index;
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int? PageSize
        {
            get
            {
                if (size.HasValue)
                {
                    if (size.Value <= 0 || size.Value > 20)
                        return 20;
                    return size;
                }
                else
                    return 20;
            }
            set
            {
                size = value;
            }
        }
        /// <summary>
        /// 页码，从0开始计算
        /// </summary>
        public int? PageIndex
        {
            get
            {
                if (index.HasValue)
                {
                    if (index.Value < 0)
                        return 0;
                    return index;
                }
                else
                    return 0;
            }
            set
            {
                index = value;
            }
        }
    }
}