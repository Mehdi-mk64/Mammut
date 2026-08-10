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
    public class SendStatus : Base.BaseEntities<byte>
    {
        #region Properies

        [Required(ErrorMessage = "وارد کردن عنوان الزامی است")]
        [StringLength(50)]
        public string Title { get; set; }

        public ICollection<MessageLog> SendStatus_MessageLog { get; set; }

        #endregion

        #region Configuration

        public class SendStatusConfiguration : IEntityTypeConfiguration<SendStatus>
        {
            public void Configure(EntityTypeBuilder<SendStatus> builder)
            {
                builder.ToTable("SendStatus", "SMS");

                builder.HasIndex(i => i.Title).IsUnique().HasDatabaseName("UK_SendStatus_Title");
            }
        }

        #endregion
    }
}
