using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Basic.SMS
{
    public class MessageLog : Base.BaseEntities<long>
    {
        #region Properies


        public long MessageSendID { get; set; }
        public MessageSend MessageLog_MessageSend { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "DateTime")]
        public DateTime ActionDateTime { get; set; }

        public byte SendStatusID { get; set; }
        public SendStatus MessageLog_SendStatus { get; set; }

        public string StatusCodeReturn { get; set; }

        public bool IsComplete { get; set; }

        public string Description { get; set; }

        public bool SendActive { get; set; }


        #endregion

        #region Configuration

        public class MessageLogConfiguration : IEntityTypeConfiguration<MessageLog>
        {
            public void Configure(EntityTypeBuilder<MessageLog> builder)
            {
                builder.ToTable("MessageLog", "SMS");
                builder.HasOne(o => o.MessageLog_MessageSend).WithMany(m => m.MessageSend_MessageLog).HasForeignKey(f => f.MessageSendID).HasConstraintName("FK_MessageLog_MessageSend_ID");
                builder.HasOne(o => o.MessageLog_SendStatus).WithMany(m => m.SendStatus_MessageLog).HasForeignKey(f => f.SendStatusID).HasConstraintName("FK_MessageLog_SendStatus_ID");

            }
        }

        #endregion
    }
}
