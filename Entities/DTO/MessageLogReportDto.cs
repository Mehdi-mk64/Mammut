using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class MessageLogReportDto
    {
        public string PersonCode { get; set; }

        public string PhoneNumber { get; set; }

        public string SendStatus { get; set; }

        public string StatusCodeReturn { get; set; }

        public DateTime ActionDateTime { get; set; }
    }
}
