using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Basic.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Basic.Personel
{
    public class Group : Base.BaseEntities<long>
    {
        #region Properies

        [Required(ErrorMessage = "وارد کردن عنوان الزامی است")]
        [StringLength(50)]
        public string Title { get; set; }



        public ICollection<AccesseGroup> Group_AccesseGroup { get; set; }

        public ICollection<PersonGroup> Group_PersonGroup { get; set; }
        #endregion

        #region Configuration

        public class GroupConfiguration : IEntityTypeConfiguration<Group>
        {
            public void Configure(EntityTypeBuilder<Group> builder)
            {
                builder.ToTable("Group", "HR");
                builder.HasIndex(i => i.Title).IsUnique().HasDatabaseName("UK_Group_Title");
            }
        }

        #endregion
    }
}
