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
        public MessageSendRepository(AppDbContext dbContext) : base(dbContext)
        {
            _messageLogRepository = new MessageLogRepository(dbContext);
        }







        private static async Task<Entities.DTO.SmsIrResault> SendBulkAsync(string message, string phone, DateTime? sendDateTime =null)
        {
            ConfigManager configManager = new ConfigManager();
            string apiKey = configManager.GetKeyValue("SmsIRService", "Key");
            SmsIr smsIr = new SmsIr(apiKey);
            long lineNumber = 10004501;
            string messageText = message;
            string[] mobile = { phone };

            int? sendDateTimeservice = null;
            DateTime inputSendDattime= sendDateTime ?? DateTime.MinValue;
            if (inputSendDattime == DateTime.MinValue)
            {
                long unixTime = new DateTimeOffset(inputSendDattime).ToUnixTimeSeconds();
                sendDateTimeservice = (int)unixTime;
            }


            var response = await smsIr.BulkSendAsync(lineNumber, message, mobile, sendDateTimeservice);
            SendResult sendResult = response.Data;
            
            Entities.DTO.SmsIrResault  smsIrResault = new Entities.DTO.SmsIrResault() 
            {
                MessageId = sendResult.MessageIds[0].GetValueOrDefault(0),
                Phone = phone,
                Status = response.Status == 1 ? true : false,
                StatusCode = response.Status
            };

            return smsIrResault;
        }



        public override async Task AddAsync(MessageSend entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            entity.InsertDateTime = DateTime.Now;

            var resaultMsg = base.AddAsync(entity, cancellationToken, saveNow);
            resaultMsg.Wait();
            string phone = entity.PhoneNummber.Nummber;

            var res = await SendBulkAsync(entity.Message, entity.PhoneNummber.Nummber, entity.DateTimeSend);


            MessageLog messageLog = new MessageLog
            {
                MessageSendID = entity.ID,
                ActionDateTime = entity.InsertDateTime,
                SendStatusID = res.Status==true ? (byte)Common.SendStatusType.API_OK:(byte)Common.SendStatusType.SendAgain,
                StatusCodeReturn = res.StatusCode.ToString(),
                SendActive = true,
                Description = "پیامک جدید"
            };

            var resaultlog = _messageLogRepository.AddAsync(messageLog, cancellationToken);

            await resaultMsg;
        }

        public override void Add(MessageSend entity, bool saveNow = true)
        {
            entity.InsertDateTime = DateTime.Now;
            base.Add(entity, saveNow);
            
            string phone = entity.PhoneNummber.Nummber;

            var res =  SendBulkAsync(entity.Message, entity.PhoneNummber.Nummber, entity.DateTimeSend);


            MessageLog messageLog = new MessageLog
            {
                MessageSendID = entity.ID,
                ActionDateTime = entity.InsertDateTime,
                SendStatusID = res.Result.Status == true ? (byte)Common.SendStatusType.API_OK : (byte)Common.SendStatusType.SendAgain,
                StatusCodeReturn = res.Result.StatusCode.ToString(),
                SendActive = true,
                Description = "پیامک جدید"
            };
            _messageLogRepository.Add(messageLog);
        }


    }
}
