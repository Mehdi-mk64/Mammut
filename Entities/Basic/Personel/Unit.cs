using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Basic.Personel
{
    public class Unit : Base.BaseEntities<long>
    {

        #region Properties

        [Required(ErrorMessage = "وارد کردن کد الزامی است")]
        [StringLength(20)]
        public string Code { get; set; }

        [Required(ErrorMessage = "وارد کردن عنوان الزامی است")]
        [StringLength(50)]
        public string Title { get; set; }
        public long? ParentUnitID { get; set; }

        [ForeignKey(nameof(ParentUnitID))]
        public Unit ParentUnit { get; set; }
        public ICollection<Unit> ChildUnit { get; set; }
        public ICollection<PersonUnit> Unit_PersonUnit { get; set; }

        #endregion


        #region Configuration

        public class UnitConfiguration : IEntityTypeConfiguration<Unit>
        {
            public void Configure(EntityTypeBuilder<Unit> builder)
            {
                builder.ToTable("Unit", "HR");

                builder.HasIndex(i => i.Code).IsUnique().HasDatabaseName("UK_Unit_Code");
                builder.HasIndex(i => i.Title).IsUnique().HasDatabaseName("UK_Unit_Title");
            }

        }

        #endregion

    }
}
