using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Basic.SMS
{
    public class SMSProvider : Base.BaseEntities<int>
    {

        #region Properties

        [Required(ErrorMessage = "وارد کردن عنوان الزامیست")]
        [StringLength(50)]
        public string Title { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string APIKey { get; set; }
        public string DomainName { get; set; }
        public string PhonSender { get; set; }
        public string MethodSendUrl { get; set; }

    
        public ICollection<MessageSend>  SmsProvider_MessageSend { get; set; }
        #endregion

        #region Configuration

        public class SMSProviderConfiguration : IEntityTypeConfiguration<SMSProvider>
        {
            public void Configure(EntityTypeBuilder<SMSProvider> builder)
            {
                builder.ToTable("SMSProvider", "SMS");

                builder.HasIndex(i => i.Title).IsUnique().HasDatabaseName("UK_SmsProvidor_Title");
                builder.HasIndex(i => i.PhonSender).IsUnique().HasDatabaseName("UK_SmsProvider_PhonSender");

            }
        }
        #endregion


    }
}
