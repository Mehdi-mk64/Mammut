using Entities.Basic.Facilities;
using Entities.Basic.Personel;
using Entities.Basic.Security;
using Entities.Basic.SMS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {

        #region DbSet

        public DbSet<Entities.Basic.Personel.Gender> Genders { get; set; }
        public DbSet<Entities.Basic.Personel.Group>  Groups { get; set; }
        public DbSet<Entities.Basic.Personel.PersonGroup> PersonelGroup { get; set; }
        public DbSet<Entities.Basic.Security.AccesseGroup> AccesseGroup { get; set; }
        public DbSet<Entities.Basic.Personel.Person> Person { get; set; }
        public DbSet<Entities.Basic.Personel.PersonPost> PersonPosts { get; set; }
        public DbSet<Entities.Basic.Personel.PersonUnit> PersonUnits { get; set; }
        public DbSet<Entities.Basic.Personel.PhonNumbers> PhonNumbers { get; set; }
        public DbSet<Entities.Basic.Personel.Post> Posts { get; set; }
        public DbSet<Entities.Basic.Personel.Unit> Units { get; set; }

        public DbSet<Entities.Basic.SMS.MessageLog> MessageLogs { get; set; }
        public DbSet<Entities.Basic.SMS.MessageSend> MessageSends { get; set; }
        public DbSet<Entities.Basic.SMS.SendImportance> SendImportances { get; set; }
        public DbSet<Entities.Basic.SMS.SendStatus> SendStatus { get; set; }
        public DbSet<Entities.Basic.SMS.SMSProvider> SMSProviders { get; set; }

        public DbSet<Entities.Basic.Facilities.InsertDataLog> InsertDataLogs { get; set; }

        public DbSet<Entities.Basic.Facilities.ViewList>   ViewLists { get; set; }
       
        public DbSet<Entities.Basic.ViewModel.ViewModelMessage> ViewModelMessages  { get; set; }


       




        #endregion

        #region Constructors

        public AppDbContext() : base()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        #endregion

        #region Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(Common.Utilities.ConfigManager.Instance.GetConnectionString());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new Gender.GenderConfiguration());
            modelBuilder.ApplyConfiguration(new Group.GroupConfiguration());
            modelBuilder.ApplyConfiguration(new Person.PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PhonNumbers.PhonNumbersConfiguration());
            modelBuilder.ApplyConfiguration(new Post.PostConfiguration());
            modelBuilder.ApplyConfiguration(new Unit.UnitConfiguration());
            modelBuilder.ApplyConfiguration(new PersonGroup.PersonGroupConfiguration());
            modelBuilder.ApplyConfiguration(new AccesseGroup.AccesseGroupConfiguration());
            modelBuilder.ApplyConfiguration(new PersonUnit.PersonUnitConfiguration());
            modelBuilder.ApplyConfiguration(new PersonPost.PersonPostConfiguration());


            modelBuilder.ApplyConfiguration(new MessageLog.MessageLogConfiguration());
            modelBuilder.ApplyConfiguration(new MessageSend.MessageSendConfiguration());
            modelBuilder.ApplyConfiguration(new SendImportance.SendImportanceConfiguration());
            modelBuilder.ApplyConfiguration(new SendStatus.SendStatusConfiguration());
            modelBuilder.ApplyConfiguration(new SMSProvider.SMSProviderConfiguration());

            modelBuilder.ApplyConfiguration(new InsertDataLog.InsertDataLogConfiguration());
            modelBuilder.ApplyConfiguration(new ViewList.ViewListConfiguration());

            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());



            #region DataSeeding


            modelBuilder.Entity<Gender>().HasData(new Entities.Basic.Personel.Gender { ID = (byte)Common.GenderType.Female, Title = Common.StringResource.StringResource.Female });
            modelBuilder.Entity<Gender>().HasData(new Entities.Basic.Personel.Gender { ID = (byte)Common.GenderType.Male, Title = Common.StringResource.StringResource.Male });
            modelBuilder.Entity<SendImportance>().HasData(new Entities.Basic.SMS.SendImportance { ID = (byte)Common.SendImportanceType.Important, Title = Common.StringResource.StringResource.Important });
            modelBuilder.Entity<SendImportance>().HasData(new Entities.Basic.SMS.SendImportance { ID = (byte)Common.SendImportanceType.Normal, Title = Common.StringResource.StringResource.Normal });
         

            modelBuilder.Entity<SendStatus>().HasData(new Entities.Basic.SMS.SendStatus { ID = (byte)Common.SendStatusType.NEWSMS, Title = Common.StringResource.StringResource.NEWSMS });
            modelBuilder.Entity<SendStatus>().HasData(new Entities.Basic.SMS.SendStatus { ID = (byte)Common.SendStatusType.SendAgain, Title = Common.StringResource.StringResource.SendAgain });
           
            modelBuilder.Entity<SendStatus>().HasData(new Entities.Basic.SMS.SendStatus { ID = (byte)Common.SendStatusType.API_OK, Title = Common.StringResource.StringResource.API_OK });
       
            modelBuilder.Entity<SendStatus>().HasData(new Entities.Basic.SMS.SendStatus { ID = (byte)Common.SendStatusType.Fault, Title = Common.StringResource.StringResource.Fault });
            modelBuilder.Entity<Person>().HasData(new Entities.Basic.Personel.Person {ID= 1, FirstName="ناشتاس",LastName="ناشناس", GenderID =2, IsActive =true, PersonCode="0" });
            modelBuilder.Entity<Person>().HasData(new Entities.Basic.Personel.Person { ID = 2, FirstName = "admin", LastName = "admin", GenderID = 2, IsActive = true, PersonCode = "1" });


            modelBuilder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>{Id = 1,Name = "Admin", NormalizedName = "ADMIN",  ConcurrencyStamp = "ADMIN-ROLE-CONCURRENCY-STAMP"});
            modelBuilder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int> { Id = 2, Name = "Users", NormalizedName = "USERS", ConcurrencyStamp = "USERS-ROLE-CONCURRENCY-STAMP" });

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            ApplicationUser admin = new ApplicationUser
            {
                Id = 1,
                PersonID = 2,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@mammut.local",
                NormalizedEmail = "ADMIN@MAMMUT.LOCAL",
                EmailConfirmed = true,
                SecurityStamp = "STATIC-ADMIN-SECURITY-STAMP",
                ConcurrencyStamp = "STATIC-ADMIN-CONCURRENCY-STAMP"
            };
            admin.PasswordHash =passwordHasher.HashPassword(admin, "Admin@123456");
            modelBuilder.Entity<ApplicationUser>().HasData(admin);
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>{UserId = 1, RoleId = 1 });

            
            ApplicationUser user = new ApplicationUser
            {
                Id = 2,
                PersonID = 1,
                UserName = "user",
                NormalizedUserName = "user",
                Email = "user@mammut.local",
                NormalizedEmail = "USER@MAMMUT.LOCAL",
                EmailConfirmed = true,
                SecurityStamp = "STATIC-USER-SECURITY-STAMP",
                ConcurrencyStamp = "STATIC-USER-CONCURRENCY-STAMP"
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "User@123456");
            modelBuilder.Entity<ApplicationUser>().HasData(user);
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int> { UserId = 2, RoleId = 2 });




            #endregion
        }

        #endregion


    }
}
