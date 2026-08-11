using Entities.Basic.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Basic.SMS

{
    public class MessageSend : Base.BaseEntities<long>
    {

        #region Properties

        [Required(ErrorMessage = "وارد کردن متن پیام الزامیست")]
        public string Message { get; set; }

        public int UserID { get; set; }

        public ApplicationUser MessageSend_User { get; set; }


        public long PhoneNumberID { get; set; }
        public Personel.PhonNumbers PhoneNummber { get; set; }

        public DateTime InsertDateTime { get; set; }
        public int MaximumTrySendSMS { get; set; }

        public DateTime? DateTimeSend { get; set; }
     

        public int? SmsProviderID { get; set; }
        public SMSProvider MessageSend_SmsProvider { get; set; }


        public byte SendImportanceID { get; set; }

        public SendImportance MessageSend_SendImportance { get; set; }

        public ICollection<MessageLog> MessageSend_MessageLog { get; set; }
        #endregion

        #region Configuration

        public class MessageSendConfiguration : IEntityTypeConfiguration<MessageSend>
        {
            public void Configure(EntityTypeBuilder<MessageSend> builder)
            {
                builder.ToTable("MessageSend", "SMS");

                builder.Property(p => p.DateTimeSend).HasColumnType("datetime").HasDefaultValueSql("getdate()");
                builder.Property(p => p.InsertDateTime).HasColumnType("DateTime").HasDefaultValueSql("getdate()");
                builder.HasOne(o => o.PhoneNummber).WithMany(m => m.MessageSend_PhoneNumer).HasForeignKey(f => f.PhoneNumberID).HasConstraintName("FK_MessageSend_PhoneNumber_ID");
                builder.HasOne(o => o.MessageSend_SmsProvider).WithMany(m => m.SmsProvider_MessageSend).HasForeignKey(f => f.SmsProviderID).HasConstraintName("FK_MessageSend_SMSProvider_ID");
                builder.HasOne(o => o.MessageSend_SendImportance).WithMany(m => m.SendImportance_MessageSend).HasForeignKey(f => f.SendImportanceID).HasConstraintName("FK_MessageSend_SendImportance_ID");
               
                builder.HasOne(x => x.MessageSend_User).WithMany(x => x.ApplicationUser_MessageSend).HasForeignKey(x => x.UserID).OnDelete(DeleteBehavior.Restrict).HasConstraintName("FK_MessageSend_ApplicationUser_ID");




            }
        }
        #endregion


    }
}
