using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Basic.Personel
{
    public class Gender : Base.BaseEntities<byte>
    {

        #region Properties

        [Required(ErrorMessage = "وارد کردن عنوان الزامیست")]
        [StringLength(50)]
        public string Title { get; set; }
        public ICollection<Person> Gender_Personnel { get; set; }

        #endregion

        #region Configuration

        public class GenderConfiguration : IEntityTypeConfiguration<Gender>
        {
            public void Configure(EntityTypeBuilder<Gender> builder)
            {
                builder.ToTable("Gender", "HR");
                builder.HasIndex(i => i.Title).IsUnique().HasDatabaseName("UK_Gender_Title");

            }
        }
        #endregion


    }
}
