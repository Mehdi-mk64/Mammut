using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{

    public class SmsIrResault
    {

        public string Phone { get; set; } 

        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public int MessageId { get; set; } 

    }

}

