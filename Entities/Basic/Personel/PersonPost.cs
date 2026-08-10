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
    public class PersonPost : Base.BaseEntities<long>
    {
        #region Properies


        public long PersonID { get; set; }
        public Person PesonPost_Person { get; set; }

        public long PostID { get; set; }             

        public Post PesonPost_Post { get; set; }

        #endregion

        #region Configuration

        public class PersonPostConfiguration : IEntityTypeConfiguration<PersonPost>
        {
    

            public void Configure(EntityTypeBuilder<PersonPost> builder)
            {
                builder.ToTable("PersonPost", "HR");
                builder.HasIndex(i => new { i.PersonID, i.PostID }).IsUnique();
                builder.HasOne(o => o.PesonPost_Person).WithMany(m => m.Person_PersonPost).HasForeignKey(f => f.PersonID).HasConstraintName("FK_PersonPost_Person_ID");
                builder.HasOne(o => o.PesonPost_Post).WithMany(m => m.Post_PersonPost).HasForeignKey(f => f.PostID).HasConstraintName("FK_PersonPost_Post_ID");

            }
        }

        #endregion
    }
}
