using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities.Basic.SMS;
using Entities.Basic.Security;

namespace Entities.Basic.Personel
{
    public class Person : Base.BaseEntities<long>
    {
        #region Constructor

        public Person()
        {
            IsActive = true;
        }

        #endregion

        #region Properties

        [Required(ErrorMessage = "وارد کردن کدپرسنلی الزامی است")]
        [StringLength(10)]
        public string PersonCode { get; set; }

        [Required(ErrorMessage = "وارد کردن نام الزامی است")]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامی است")]
        [StringLength(50)]
        public string LastName { get; set; }
        public byte GenderID { get; set; }
        public Gender Personnel_Gender { get; set; }
        public bool IsActive { get; set; }

        


        public ICollection<PersonUnit> Person_PersonUnit { get; set; }
        public ICollection<PersonPost> Person_PersonPost { get; set; }

        public ICollection<PhonNumbers> Person_PhoneNumbers { get; set; }
        public ICollection<PersonGroup> Person_PersonGroup { get; set; }

       
        #endregion

        #region Configuration

        public class PersonConfiguration : IEntityTypeConfiguration<Person>
        {
            public void Configure(EntityTypeBuilder<Person> builder)
            {
                builder.ToTable("Person", "HR");

                builder.HasIndex(i => i.PersonCode).IsUnique().HasDatabaseName("UK_Person_PersonCode");
                builder.HasOne(o => o.Personnel_Gender).WithMany(m => m.Gender_Personnel).HasForeignKey(f => f.GenderID).HasConstraintName("FK_Person_Gender_ID");

            }
        }

        #endregion

    }

}
