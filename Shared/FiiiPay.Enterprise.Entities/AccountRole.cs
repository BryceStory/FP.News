using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Entities
{
    public class AccountRole
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime? ModifyTime { get; set; }
        public Guid? ModifyBy { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
