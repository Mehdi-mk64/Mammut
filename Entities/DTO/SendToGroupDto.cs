using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class SendToGroupDto
    {
        public string Message { get; set; }
        public long GroupID { get; set; }

        public DateTime? DateTimeSend { get; set; } = null;

        public byte SendImportanceID { get; set; }

        public int? SmsProviderID { get; set; }
    }
}
