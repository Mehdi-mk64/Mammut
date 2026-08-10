using Entities.Basic.Personel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Basic.Security
{
    public class AccesseGroup : Base.BaseEntities<long>
    {
        #region Properies
        public int UserID { get; set; }
        public ApplicationUser AccesseGroup_ApplicationUser { get; set; }

        public long GroupID { get; set; }
        public Group AccesseGroup_Group { get; set; }
        #endregion

        #region Configuration

        public class AccesseGroupConfiguration : IEntityTypeConfiguration<AccesseGroup>
        {
            public void Configure(EntityTypeBuilder<AccesseGroup> builder)
            {
                builder.ToTable("AccesseGroup", "Security");
                builder.HasIndex(i => new { i.UserID, i.GroupID }).IsUnique();
                builder.HasOne(o => o.AccesseGroup_ApplicationUser).WithMany(m => m.ApplicationUser_AccesseGroup).HasForeignKey(f => f.UserID).HasConstraintName("FK_AccesseGroup_User_ID").OnDelete(DeleteBehavior.Cascade); ;
                builder.HasOne(o => o.AccesseGroup_Group).WithMany(m => m.Group_AccesseGroup).HasForeignKey(f => f.GroupID).HasConstraintName("FK_AccesseGroup_Group_ID").OnDelete(DeleteBehavior.Cascade); ;

            }


        }

        #endregion

    }
}
