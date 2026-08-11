using Common.Security;
using DAL.Repository.Base;
using DAL.Repository.Basic.Personal;
using DAL.Repository.Basic.Security;
using Entities.Basic.Security;
using Entities.Basic.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Middleware;
using WebFramework.StartupConfig;


namespace SystemManagment
{
    public class Startup
    {


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<DAL.AppDbContext>(ServiceLifetime.Transient);

            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<DAL.AppDbContext>()
                .AddDefaultTokenProviders();


            services.Configure<JwtSettings>( Configuration.GetSection("JwtSettings"));
            
            services.AddJWTAuthentication(Configuration);

            services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.Personel.Gender>), typeof(DAL.Repository.Basic.Personal.GenderRepository));


            services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.Personel.Person>), typeof(DAL.Repository.Basic.Personal.PersonRepository));
            services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.Personel.PhonNumbers>), typeof(DAL.Repository.Basic.Personal.PhonNummberRepository));
            services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.Personel.Post>), typeof(DAL.Repository.Basic.Personal.PostRepository));
            services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.Personel.Unit>), typeof(DAL.Repository.Basic.Personal.UnitRepository));
            services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.Personel.PersonGroup>), typeof(DAL.Repository.Basic.Personal.PersonGroupRepository));
            services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.Personel.PersonPost>), typeof(DAL.Repository.Basic.Personal.PersonPostRepository));
            services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.Personel.PersonUnit>), typeof(DAL.Repository.Basic.Personal.PersonUnitRepository));

            services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.Security.AccesseGroup>),typeof(DAL.Repository.Basic.Security.AccesseGroupRepository));
            services.AddScoped<DAL.Repository.Basic.Security.AccesseGroupRepository>();

            services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.Personel.Group>), typeof(DAL.Repository.Basic.Personal.GroupRepository));
            services.AddScoped<DAL.Repository.Basic.Personal.GroupRepository>();
            
            services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.SMS.MessageSend>), typeof(DAL.Repository.Basic.SMS.MessageSendRepository));
            services.AddScoped<DAL.Repository.Basic.SMS.MessageSendRepository>();


            services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.SMS.MessageLog>), typeof(DAL.Repository.Basic.SMS.MessageLogRepository));
            services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.SMS.SendImportance>), typeof(DAL.Repository.Basic.SMS.SendImportanceRepository));
            services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.SMS.SendStatus>), typeof(DAL.Repository.Basic.SMS.SendStatusRepository));
            services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.SMS.SMSProvider>), typeof(DAL.Repository.Basic.SMS.SendProviderRepository));
           // services.AddScoped(typeof(DAL.Repository.IRepository<Entities.Basic.SMS.GSMSender>), typeof(DAL.Repository.Basic.SMS.GSMSenderRepository));


            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

       
            services.AddJobSchedule();

            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(new AuthorizeFilter());
            //}).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCustomExceptionHandler();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();



            app.UseCors("AllowOrigin");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
