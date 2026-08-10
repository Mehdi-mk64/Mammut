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
    public class Post : Base.BaseEntities<long>
    {
        #region Properies

        [Required(ErrorMessage = "وارد کردن عنوان الزامی است")]
        [StringLength(50)]
        public string Title { get; set; }

        public short CodeGroup { get; set; }

        public ICollection<PersonPost> Post_PersonPost { get; set; }

        #endregion

        #region Configuration

        public class PostConfiguration : IEntityTypeConfiguration<Post>
        {
            public void Configure(EntityTypeBuilder<Post> builder)
            {
                builder.ToTable("Post", "HR");

                builder.HasIndex(i => i.Title).IsUnique().HasDatabaseName("UK_Post_Title");
            }
        }

        #endregion
    }
}
