using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Services.Jobs;
using Common.Utilities;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services.View;

namespace WebFramework.StartupConfig
{

    public static class ScheduleConfiguration
    {
        public static void AddJobSchedule(this IServiceCollection services)
        {

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<SMSIRSendSMSJob>();
            services.AddSingleton(new JobSchedule(jobType: typeof(SMSIRSendSMSJob), cronExpression: ConfigManager.Instance.GetKeyValue("JobScheduleTime", "SMSIRSendSMSJob")));


            //services.AddSingleton<GSMSendSMSJob>();
            //services.AddSingleton(new JobSchedule(jobType: typeof(GSMSendSMSJob), cronExpression: ConfigManager.Instance.GetKeyValue("JobScheduleTime", "GSMSendSMS")));


            services.AddSingleton<InsertDataJob>();
            services.AddSingleton(new JobSchedule(jobType: typeof(InsertDataJob), cronExpression: ConfigManager.Instance.GetKeyValue("JobScheduleTime", "ViewInsert")));


            services.AddHostedService<QuartzHostedService>();




        }
    }
}
