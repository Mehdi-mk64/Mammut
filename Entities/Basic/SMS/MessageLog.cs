using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Basic.SMS
{
    public class MessageLog : Base.BaseEntities<long>
    {
        #region Properies

        
        public long MessageSendPhoneID { get; set; }
        public MessageSendPhone MessageLog_MessageSendPhone { get; set; }

   
        public DateTime DateAction { get; set; }
        public TimeSpan TimeAction { get; set; }


        public byte SendStatusID { get; set; }
        public SendStatus MessageLog_SendStatus { get; set; }


        public string StatusCodeReturn { get; set; }

        public bool IsComplete { get; set;}

        public string Description { get; set; }

        
       
        #endregion

        #region Configuration

        public class MessageLogConfiguration : IEntityTypeConfiguration<MessageLog>
        {
            public void Configure(EntityTypeBuilder<MessageLog> builder)
            {
                builder.ToTable("MessageLog", "SMS");
                builder.Property(p => p.DateAction).HasColumnType("date").HasDefaultValueSql("getdate()");
                builder.Property(p => p.TimeAction).HasColumnType("time").HasDefaultValueSql("getdate()");
                builder.HasOne(o => o.MessageLog_SendStatus).WithMany(m => m.SendStatus_MessageLog).HasForeignKey(f => f.SendStatusID).HasConstraintName("FK_MessageLog_SendStatus_ID");
                builder.HasOne(o => o.MessageLog_MessageSendPhone).WithMany(m => m.MessageSendPhone_MessageLog).HasForeignKey(f => f.MessageSendPhoneID).HasConstraintName("FK_MessageLog_MessageSendPhone_ID");


            }
        }

        #endregion
    }
}
