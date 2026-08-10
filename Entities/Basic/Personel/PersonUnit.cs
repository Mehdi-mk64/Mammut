using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Basic.Personel
{
    public class PersonUnit : Base.BaseEntities<long>
    {
        #region Properies

        public long PersonID { get; set; }
        public Person PersonUnit_Person { get; set; }

        public long UnitID { get; set; }
        public Unit PersonUnit_Unit { get; set; }

        #endregion

        #region Configuration

        public class PersonUnitConfiguration : IEntityTypeConfiguration<PersonUnit>
        {
            public void Configure(EntityTypeBuilder<PersonUnit> builder)
            {
                builder.ToTable("PersonUnit", "HR");
                builder.HasIndex(i => new { i.PersonID, i.UnitID }).IsUnique();
                builder.HasOne(o => o.PersonUnit_Person).WithMany(m => m.Person_PersonUnit).HasForeignKey(f => f.PersonID).HasConstraintName("FK_PersonUnit_Person_ID");
                builder.HasOne(o => o.PersonUnit_Unit).WithMany(m => m.Unit_PersonUnit).HasForeignKey(f => f.UnitID).HasConstraintName("FK_PersonUnit_Unit_ID");

            }
        }

        #endregion
    }
}
