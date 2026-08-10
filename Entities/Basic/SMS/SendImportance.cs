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
    public class SendImportance : Base.BaseEntities<byte>
    {

        #region Properties

        [Required(ErrorMessage = "وارد کردن عنوان الزامیست")]
        [StringLength(50)]
        public string Title { get; set; }
        public ICollection<MessageSend> SendImportance_MessageSend { get; set; }

        #endregion

        #region Configuration

        public class SendImportanceConfiguration : IEntityTypeConfiguration<SendImportance>
        {
            public void Configure(EntityTypeBuilder<SendImportance> builder)
            {
                builder.ToTable("SendImportance", "SMS");

                builder.HasIndex(i => i.Title).IsUnique().HasDatabaseName("UK_Gender_Title");

            }
        }
        #endregion


    }
}
