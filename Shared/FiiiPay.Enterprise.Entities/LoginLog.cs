using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiiiPay.Enterprise.Entities
{
   public class LoginLog
    {
        //[], [], [], [], [], [], [], [], [], [], []
        public long Id { get; set; }

        public Guid AccountId { get; set; }

        public string IP { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string OS { get; set; }

        public string OSVersion { get; set; }

        public string AppVersion { get; set; }

        public DateTime Timestamp { get; set; }

        public bool IsSuccess { get; set; }

        public string Remark { get; set; }
    }
}
