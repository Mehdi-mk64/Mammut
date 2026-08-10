using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using DAL.Repository.Basic;
using Quartz;
using Services.SMS;

namespace Services.Jobs
{

    [DisallowConcurrentExecution]
    public class MagfaSendSMSJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            
            SMSSend smsSend = new SMSSend();
            
            await smsSend.MagfaSendSMS();

        }
    }

    [DisallowConcurrentExecution]
    public class GSMSendSMSJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {

            SMSSend smsSend = new SMSSend();
            await smsSend.GsmSendSMS();

        }
    }


    [DisallowConcurrentExecution]
    public class InsertDataJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            View.ViewInsert viewInsert = new View.ViewInsert();
            await viewInsert.InsertDataFromView();

        }
    }
}
