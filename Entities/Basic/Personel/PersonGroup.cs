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
    public class PersonGroup : Base.BaseEntities<long>
    {
        #region Properies
       public long PersonID { get; set; }
        public Person PersonGroup_Person { get; set; }

        public long GroupID { get; set; }
        public Group PersonGroup_Group { get; set; }
        #endregion

        #region Configuration

        public class PersonGroupConfiguration : IEntityTypeConfiguration<PersonGroup>
        {
            public void Configure(EntityTypeBuilder<PersonGroup> builder)
            {
                builder.ToTable("PersonGroup","HR");
                builder.HasIndex(i => new { i.PersonID, i.GroupID }).IsUnique();
                builder.HasOne(o => o.PersonGroup_Person).WithMany(m => m.Person_PersonGroup).HasForeignKey(f => f.PersonID).HasConstraintName("FK_PersonGroup_Person_ID");
                builder.HasOne(o => o.PersonGroup_Group).WithMany(m => m.Group_PersonGroup).HasForeignKey(f => f.GroupID).HasConstraintName("FK_PersonGroup_Group_ID");

            }
        }

        #endregion
    }
}
