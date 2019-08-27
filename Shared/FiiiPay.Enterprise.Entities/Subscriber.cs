using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Entities
{
    public class Subscriber
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
