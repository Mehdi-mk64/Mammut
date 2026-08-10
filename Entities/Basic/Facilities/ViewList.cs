using Entities.Basic.Personel;
using Entities.Basic.SMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities.Basic.Facilities
{
    public class ViewList : Base.BaseEntities<long>
    {

        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Common.StringResource.StringResource))]
        [MaxLength(20, ErrorMessageResourceName = "MaxErrorMessage", ErrorMessageResourceType = typeof(Common.StringResource.StringResource))]
        public string SchemaName { get; set; }


        [Required(ErrorMessageResourceName = "RequiredErrorMessage", ErrorMessageResourceType = typeof(Common.StringResource.StringResource))]
        [MaxLength(20, ErrorMessageResourceName = "MaxErrorMessage", ErrorMessageResourceType = typeof(Common.StringResource.StringResource))]
        public string ViewName { get; set; }

        public ICollection<InsertDataLog> ViewList_InsertDataLog { get; set; }

        public class ViewListConfiguration : IEntityTypeConfiguration<ViewList>
        {
            public void Configure(EntityTypeBuilder<ViewList> builder)
            {
                builder.ToTable("ViewList", "Service");
                builder.HasIndex(i => new { i.SchemaName, i.ViewName }).IsUnique().HasDatabaseName("UK_ViweList_View");

            }

        }

    }
    
}
