using Entities.Base;
using Entities.Basic.Personel;
using Entities.Basic.SMS;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;


namespace Entities.Basic.Security
{
    public class ApplicationUser : IdentityUser<int>
    {
        public long PersonID { get; set; }
        public virtual Person Person { get; set; }

        public ICollection<AccesseGroup> ApplicationUser_AccesseGroup { get; set; }

        public ICollection<MessageSend> ApplicationUser_MessageSend { get; set; }

    }


    #region Configuration


    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {


        public void Configure(EntityTypeBuilder<ApplicationUser> builder)

        {
            builder.ToTable("ApplicationUser", "Security");

            builder.HasOne(u => u.Person)
                .WithOne()
                .HasForeignKey<ApplicationUser>(u => u.PersonID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }


    #endregion

}
