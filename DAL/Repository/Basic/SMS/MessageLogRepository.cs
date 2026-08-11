
using Common;
using Entities.Basic.SMS;
using Entities.DTO;
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



        public async Task<PagedResultDto<MessageLogReportDto>> SearchReportAsync( string? phoneNumber, string? personCode,
            int page, int pageSize, CancellationToken cancellationToken = default)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 15;

            if (pageSize > 50)
                pageSize = 50;

            var query = TableNoTracking.AsQueryable();

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                phoneNumber = phoneNumber.Trim();

                query = query.Where(x =>
                    x.MessageLog_MessageSend
                        .PhoneNummber
                        .Nummber
                        .Contains(phoneNumber));
            }

            if (!string.IsNullOrWhiteSpace(personCode))
            {
                personCode = personCode.Trim();

                query = query.Where(x =>
                    x.MessageLog_MessageSend
                        .PhoneNummber
                        .Phone_Person
                        .PersonCode
                        .Contains(personCode));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                // جدیدترین به قدیمی‌ترین
                .OrderByDescending(x => x.ActionDateTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new MessageLogReportDto
                {
                    PersonCode = x.MessageLog_MessageSend.PhoneNummber.Phone_Person.PersonCode,

                    PhoneNumber =x.MessageLog_MessageSend.PhoneNummber.Nummber,

                    SendStatus = x.MessageLog_SendStatus.Title,

                    StatusCodeReturn = x.StatusCodeReturn,

                    ActionDateTime = x.ActionDateTime
                })

                .ToListAsync(cancellationToken);

            return new PagedResultDto<MessageLogReportDto>
            {
                Items = items,

                Page = page,

                PageSize = pageSize,

                TotalCount = totalCount
            };
        }

        public async Task<PagedResultDto<MessageLogReportDto>> SearchReportAsync( string? phoneNumber, string? personCode,
             int page, int pageSize, int currentUserID, bool isAdmin, CancellationToken cancellationToken = default)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 15;

            if (pageSize > 50)
                pageSize = 50;

            var query = TableNoTracking.AsQueryable();

            // User معمولی فقط Log پیام‌هایی را می‌بیند
            // که خودش ارسال کرده است.
            // Admin همه را می‌بیند.
            if (!isAdmin)
            {
                query = query.Where(x => x.MessageLog_MessageSend.UserID == currentUserID);
            }

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                phoneNumber = phoneNumber.Trim();

                query = query.Where(x =>
                    x.MessageLog_MessageSend
                        .PhoneNummber
                        .Nummber
                        .Contains(phoneNumber));
            }

            if (!string.IsNullOrWhiteSpace(personCode))
            {
                personCode = personCode.Trim();

                query = query.Where(x =>
                    x.MessageLog_MessageSend
                        .PhoneNummber
                        .Phone_Person
                        .PersonCode
                        .Contains(personCode));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                // جدیدترین به قدیمی‌ترین
                .OrderByDescending(x => x.ActionDateTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new MessageLogReportDto
                {
                    PersonCode =x.MessageLog_MessageSend.PhoneNummber.Phone_Person.PersonCode,
                    PhoneNumber = x.MessageLog_MessageSend.PhoneNummber.Nummber,
                    SendStatus =x.MessageLog_SendStatus.Title,
                    StatusCodeReturn =x.StatusCodeReturn,
                    ActionDateTime = x.ActionDateTime
                }).ToListAsync(cancellationToken);

            return new PagedResultDto<MessageLogReportDto>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }



    }
}
