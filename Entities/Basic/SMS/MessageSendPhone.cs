using Entities.Basic.Personel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Basic.SMS

{
    public class MessageSendPhone  : Base.BaseEntities<long>
    {

        #region Properties

        
        public long PhoneNumberID { get; set; }
       

        public Personel.PhonNumbers MessageSendPhone_PhonNumbers { get; set; }


        public long MessageSendID { get; set; }

        public  MessageSend MessageSendPhone_MessageSend { get; set; }


        public ICollection<MessageLog> MessageSendPhone_MessageLog { get; set; }


        #endregion



        #region Configuration

        public class MessageSendPhoneConfiguration : IEntityTypeConfiguration<MessageSendPhone>
        {
            public void Configure(EntityTypeBuilder<MessageSendPhone> builder)
            {
                builder.ToTable("MessageSendPhone", "SMS");
                             
                builder.HasOne(o => o.MessageSendPhone_PhonNumbers).WithMany(m => m.PhonNumbers_MessageSendPhone).HasForeignKey(f => f.PhoneNumberID).HasConstraintName("FK_MessageSendPhone_PhoneNumber_ID").OnDelete(DeleteBehavior.NoAction);

                builder.HasOne(o => o.MessageSendPhone_MessageSend).WithMany(m => m.MessageSend_MessageSendPhone).HasForeignKey(f => f.MessageSendID).HasConstraintName("FK_MessageSendPhone_MessageSendID_ID").OnDelete(DeleteBehavior.Cascade);

            
            }
        }
        #endregion


    }
}
