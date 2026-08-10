using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Basic.SMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Basic.Personel
{
    public class PhonNumbers : Base.BaseEntities<long>
    {
        #region Properies

        [Required(ErrorMessage = "وارد کردن عنوان الزامی است")]
        [StringLength(50)]
        public string Nummber { get; set; }
        public long PersonID { get; set; }
        public Person Phone_Person { get; set; }
        #endregion
        public ICollection<MessageSend> MessageSend_PhoneNumer { get; set; }


        public ICollection<MessageSendPhone> PhonNumbers_MessageSendPhone { get; set; }

        #region Configuration

        public class PhonNumbersConfiguration : IEntityTypeConfiguration<PhonNumbers>
        {
            public void Configure(EntityTypeBuilder<PhonNumbers> builder)
            {
                builder.ToTable("PhonNumbers", "HR");
                builder.HasIndex(i => i.Nummber).IsUnique().HasDatabaseName("UK_PhoneNumber_Number");
                builder.HasOne(o => o.Phone_Person).WithMany(m => m.Person_PhoneNumbers).HasForeignKey(f => f.PersonID).HasConstraintName("FK_PhoneNumber_Person_ID").OnDelete(DeleteBehavior.Cascade);
            }
        }

        #endregion
    }
}
