using Common;
using Common.Utilities;
using DAL.Repository.Basic.Personal;
using Entities.Basic.Personel;
using Entities.Basic.Security;
using Entities.Basic.SMS;
using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Spire.Pdf.Exporting.XPS.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Repository.Basic.SMS

{
    public class MessageSendRepository : Repository<MessageSend>
    {
        private MessageLogRepository _messageLogRepository;

        private AccesseGroupRepository _accesseGroupRepository;
        private PhonNummberRepository _phoneNumbersRepository;


        public MessageSendRepository(AppDbContext dbContext) : base(dbContext)
        {
            _messageLogRepository = new MessageLogRepository(dbContext);


        }




        private static async Task<List<Entities.DTO.SmsIrResault>> SendBulkAsync(string apiKey, string message, List<string> phons, string? sendDateTime = null)
        {
            SmsIr smsIr = new SmsIr(apiKey);
            long lineNumber = 10004501;
            string messageText = message;
            string[] mobiles = phons.ToArray();

            int? sendDateTimeservice = null;
            DateTime.TryParse(sendDateTime, out DateTime inputSendDattime);
            if (inputSendDattime == DateTime.MinValue)
            {
                long unixTime = new DateTimeOffset(inputSendDattime).ToUnixTimeSeconds();
                sendDateTimeservice = (int)unixTime;
            }


            var response = await smsIr.BulkSendAsync(lineNumber, messageText, mobiles, sendDateTimeservice);
            SendResult sendResult = response.Data;
            List<Entities.DTO.SmsIrResault> smsIrResault = new List<Entities.DTO.SmsIrResault>();
            byte statusCode = response.Status;
            for (int i = 0; i < sendResult.MessageIds.Length; i++)
            {
                smsIrResault.Add(new Entities.DTO.SmsIrResault
                {
                    MessageId = sendResult.MessageIds[i].GetValueOrDefault(0),
                    Phone = phons[i],
                    Status = statusCode == 1 ? true : false,
                    StatusCode = statusCode
                });
            }
            return smsIrResault;

        }


        public override async Task AddAsync(MessageSend entity, CancellationToken cancellationToken, bool saveNow = true)
        {

            //var phoneList = await _accesseGroupRepository.GetUserGroupsAsync(entity.UserID , cancellationToken);

            //if (phoneList == null)
            //    throw new UnauthorizedAccessException("خطا در دسترسی");

            entity.InsertDateTime = DateTime.Now;
            
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);

            

            MessageLog messageLog = new MessageLog() 
            { 
             
            };

            
            await _messageLogRepository.AddAsync(entity, cancellationToken);
            
        }

        public override Task AddRangeAsync(IEnumerable<MessageSend> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            var phones = entities.Select(s => s.MessageSend_MessageSendPhone);
            //SendBulkAsync(string apiKey, string message, List<string> phons, sendDateTime = null)
            return base.AddRangeAsync(entities, cancellationToken, saveNow);
        }


        //public override void Add(MessageSend entity, bool saveNow = true)
        //{
        //    entity.InsertDateTime = DateTime.Now;
        //    base.Add(entity, saveNow);
        //    MessageLog messageLog = new MessageLog
        //    {
        //        MessageSendID = entity.ID,
        //        ActionDateTime = entity.InsertDateTime,
        //        SendStatusID = (entity.SendImportanceID == (byte)SendImportanceType.ForceGSM ? (byte)Common.SendStatusType.SendGSM : (byte)Common.SendStatusType.NEWSMS),
        //        StatusCodeReturn = "InsertNew",
        //        SendActive = true,
        //        Description = "پیامک جدید"
        //    };

        //    _messageLogRepository.Add(messageLog);
        //}



    }
}
