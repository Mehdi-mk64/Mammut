
using Common;
using Entities.Basic.SMS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repository.Basic.SMS

{
    public class MessageLogRepository : Repository<MessageLog>
    {
        public MessageLogRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<List<MessageLog>> GetSmsSendListWithApi(CancellationToken cancellationToken)
        {
            
            return await Table.Include(i => i.MessageLog_MessageSend).ThenInclude(ti => ti.PhoneNummber)
                .Include(i => i.MessageLog_MessageSend).ThenInclude(ti => ti.MessageSend_SmsProvider)
                .Include(i => i.MessageLog_MessageSend).ThenInclude(ti => ti.MessageSend_SendImportance)
                .Where(w => (w.SendActive == true)
                            && (w.SendStatusID == (byte)Common.SendStatusType.NEWSMS || w.SendStatusID == (byte)Common.SendStatusType.SendAgain)
                            && w.MessageLog_MessageSend.DateTimeSend < DateTime.Now.Date
                            //&& TimeSpan.Compare(w.MessageLog_MessageSend.TimeSend, timeNow) <= 0
                            )
                .ToListAsync(cancellationToken);

        }



        public async Task<List<MessageLog>> GetMessageStatusListByID(long messageSendID, SendStatusType sendStatusType, CancellationToken cancellationToken)
        {
            return Table.Include(i => i.MessageLog_MessageSend).ThenInclude(ti => ti.MessageSend_SendImportance)
                .Where(w => w.MessageSendID == messageSendID && w.SendStatusID == (byte)sendStatusType)
                .ToList();

        }



    }
}
