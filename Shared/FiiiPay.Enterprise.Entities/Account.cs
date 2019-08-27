using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Entities
{
    public class Account
    {
        public Guid Id { get; set; }

        public string Cellphone { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string MerchantName { get; set; }

        public string Password { get; set; }

        public string PIN { get; set; }

        public byte Status { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime? ModifyTime { get; set; }
        public int? RoleId { get; set; }
        public Guid CreateBy { get; set; }
        public Guid ModifyBy { get; set; }
    }
}
