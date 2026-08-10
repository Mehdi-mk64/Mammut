
using Entities.Basic.SMS;
using System.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Common;

namespace DAL.Repository.Basic.SMS

{
    public class MessageLogRepository : Repository<MessageLog>
    {
        public MessageLogRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


        //public async Task<List<MessageLog>> GetSmsSendListWithApi(CancellationToken cancellationToken)
        //{
        //    TimeSpan timeNow = DateTime.Now.TimeOfDay;
        //    return await Table.Include(i => i.MessageLog_MessageSend).ThenInclude(ti=>ti.PhoneNummber )
        //        .Include(i=>i.MessageLog_MessageSend).ThenInclude(ti=>ti.MessageSend_SmsProvider)
        //        .Include(i => i.MessageLog_MessageSend).ThenInclude(ti => ti.MessageSend_SendImportance)
        //        .Where(w => (w.SendActive==true)
        //                    && (w.SendStatusID == (byte)Common.SendStatusType.NEWSMS || w.SendStatusID == (byte)Common.SendStatusType.SendAgain)
        //                    && w.MessageLog_MessageSend.DateSend <= DateTime.Now.Date 
        //                    && TimeSpan.Compare(w.MessageLog_MessageSend.TimeSend,timeNow) <=0
        //                    )
        //        .ToListAsync(cancellationToken);
            
        //}

        //public async Task<List<MessageLog>> GetSmsSendListWithGSM (CancellationToken cancellationToken)
        //{
        //    var res= await Table.Include(i => i.MessageLog_MessageSend).ThenInclude(ti => ti.PhoneNummber)
        //        .Include(i => i.MessageLog_MessageSend).ThenInclude(ti => ti.MessageSend_GSMSender)
        //        .Include(i => i.MessageLog_MessageSend).ThenInclude(ti => ti.MessageSend_SendImportance)
        //        .Where(w => w.SendActive == true && w.MessageLog_MessageSend.DateSend <= DateTime.Now.Date 
        //                                         && w.MessageLog_MessageSend.TimeSend <= DateTime.Now.TimeOfDay
        //                                         && (w.SendStatusID == (byte)Common.SendStatusType.SendGSM))
        //        .ToListAsync(cancellationToken);

        //    return res;
        //}

        //public async Task<List<MessageLog>> GetMessageStatusListByID(long messageSendID, SendStatusType sendStatusType,CancellationToken cancellationToken)
        //{
        //    return Table.Include(i => i.MessageLog_MessageSend).ThenInclude(ti => ti.MessageSend_SendImportance)
        //        .Where(w => w.MessageSendID==messageSendID && w.SendStatusID == (byte) sendStatusType)
        //        .ToList();
            
        //}

     

    }
}
