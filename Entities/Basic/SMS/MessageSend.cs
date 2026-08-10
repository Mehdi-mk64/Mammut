using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Basic.SMS

{
    public class MessageSend  : Base.BaseEntities<long>
    {

        #region Properties

        [Required(ErrorMessage = "وارد کردن متن پیام الزامیست")]
        public string Message { get; set; }

        //public long PhoneNumberID { get; set; }
        //public Personel.PhonNumbers PhoneNummber { get; set; }

        public DateTime InsertDateTime { get; set; }
        public int MaximumTrySendSMS { get; set; }



        public int? SmsProviderID { get; set; }
        public SMSProvider MessageSend_SmsProvider { get; set; }

        public byte SendImportanceID { get; set; }

        public SendImportance MessageSend_SendImportance { get; set; }

        public int UserID { get; set; }
        public Security.ApplicationUser MessageSend_ApplicationUser { get; set; }


        #endregion

        public ICollection<MessageSendPhone> MessageSend_MessageSendPhone { get; set; }


        #region Configuration

        public class MessageSendConfiguration : IEntityTypeConfiguration<MessageSend>
        {
            public void Configure(EntityTypeBuilder<MessageSend> builder)
            {
                builder.ToTable("MessageSend", "SMS");

          
                builder.Property(p => p.InsertDateTime).HasColumnType("DateTime").HasDefaultValueSql("getdate()");
                //builder.HasOne(o => o.PhoneNummber).WithMany(m => m.MessageSend_PhoneNumer).HasForeignKey(f => f.PhoneNumberID).HasConstraintName("FK_MessageSend_PhoneNumber_ID");
                builder.HasOne(o => o.MessageSend_SmsProvider).WithMany(m => m.SmsProvider_MessageSend).HasForeignKey(f => f.SmsProviderID).HasConstraintName("FK_MessageSend_SMSProvider_ID");
                builder.HasOne(o => o.MessageSend_SendImportance).WithMany(m => m.SendImportance_MessageSend).HasForeignKey(f => f.SendImportanceID).HasConstraintName("FK_MessageSend_SendImportance_ID");
                builder.HasOne(o => o.MessageSend_ApplicationUser).WithMany(m => m.ApplicationUser_MessageSend).HasForeignKey(f => f.UserID).HasConstraintName("FK_MessageSend_User_ID");


            }
        }
        #endregion


    }
}
