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







        private static async Task<Entities.DTO.SmsIrResault> SendBulkAsync(string message, string phone, DateTime? sendDateTime = null)
        {
            ConfigManager configManager = new ConfigManager();
            string apiKey = configManager.GetKeyValue("SmsIRService", "Key");
            SmsIr smsIr = new SmsIr(apiKey);
            long lineNumber = 10004501;
            long.TryParse(configManager.GetKeyValue("SmsIRService", "Nummber"), out lineNumber);
            string messageText = message;
            string[] mobile = { phone };

            int? sendDateTimeservice = null;

            if (sendDateTime.HasValue)
            {
                long unixTime =new DateTimeOffset(sendDateTime.Value).ToUnixTimeSeconds();
                sendDateTimeservice = (int)unixTime;
            }


            var response = await smsIr.BulkSendAsync(lineNumber, message, mobile, sendDateTimeservice);

            if (response == null)
            {
                throw new Exception("پاسخی از سرویس SMS.ir دریافت نشد.");
            }

            if (response.Data == null)
            {
                return new Entities.DTO.SmsIrResault
                {
                    Phone = phone,
                    MessageId = 0,
                    Status = false,
                    StatusCode = response.Status
                };
            }


            var sendResult = response.Data;

            return new Entities.DTO.SmsIrResault
            {
                MessageId = sendResult.MessageIds != null && sendResult.MessageIds.Length > 0 ? sendResult.MessageIds[0].GetValueOrDefault(0) : 0,
                Phone = phone,
                Status = response.Status == 1,
                StatusCode = response.Status
            };

        }



        //public override async Task AddAsync(MessageSend entity, CancellationToken cancellationToken, bool saveNow = true)
        //{
        //    entity.InsertDateTime = DateTime.Now;

        //    var resaultMsg = base.AddAsync(entity, cancellationToken, saveNow);
        //    resaultMsg.Wait();
        //    string phone = entity.PhoneNummber.Nummber;

        //    var res = await SendBulkAsync(entity.Message, entity.PhoneNummber.Nummber, entity.DateTimeSend);


        //    MessageLog messageLog = new MessageLog
        //    {
        //        MessageSendID = entity.ID,
        //        ActionDateTime = entity.InsertDateTime,
        //        SendStatusID = res.Status==true ? (byte)Common.SendStatusType.API_OK:(byte)Common.SendStatusType.SendAgain,
        //        StatusCodeReturn = res.StatusCode.ToString(),
        //        SendActive = true,
        //        Description = "پیامک جدید"
        //    };

        //    var resaultlog = _messageLogRepository.AddAsync(messageLog, cancellationToken);

        //    await resaultMsg;
        //}

        public async Task<Entities.DTO.SmsIrResault> SendAsync(MessageSend entity, CancellationToken cancellationToken)
        {
            entity.InsertDateTime = DateTime.Now;

            var phone = await TableNoTrackingOf<PhonNumbers>()
                .Where(x => x.ID == entity.PhoneNumberID)
                .Select(x => x.Nummber)
                .FirstOrDefaultAsync(cancellationToken);

            if (string.IsNullOrWhiteSpace(phone))
                throw new Exception("شماره تلفن موردنظر پیدا نشد.");

            await base.AddAsync(entity,cancellationToken,saveNow: true);
            try
            {
                var result = await SendBulkAsync(entity.Message,phone, entity.DateTimeSend);

                var messageLog = new MessageLog
                {
                    MessageSendID = entity.ID,
                    ActionDateTime = DateTime.Now,

                    SendStatusID = result.Status ? (byte)Common.SendStatusType.API_OK : (byte)Common.SendStatusType.SendAgain,

                    StatusCodeReturn = result.StatusCode.ToString(),
                    SendActive = true,
                    IsComplete = result.Status,

                    Description = result.Status ? "پیامک با موفقیت ارسال شد."  : "ارسال پیامک ناموفق بود."
                };

                await _messageLogRepository.AddAsync(messageLog, cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                var messageLog = new MessageLog
                {
                    MessageSendID = entity.ID,
                    ActionDateTime = DateTime.Now,
                    SendStatusID = (byte)Common.SendStatusType.SendAgain,
                    StatusCodeReturn = "EXCEPTION",
                    SendActive = true,
                    IsComplete = false,
                    Description = ex.Message
                };

                await _messageLogRepository.AddAsync(messageLog, cancellationToken);

                throw;
            }
        }







        public override async Task AddAsync(MessageSend entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            entity.InsertDateTime = DateTime.Now;

            // شماره تلفن را با PhoneNumberID پیدا می‌کنیم
            var phone = await TableNoTrackingOf<PhonNumbers>()
                .Where(x => x.ID == entity.PhoneNumberID)
                .Select(x => x.Nummber)
                .FirstOrDefaultAsync(cancellationToken);

            if (string.IsNullOrWhiteSpace(phone))
                throw new Exception("شماره تلفن موردنظر پیدا نشد.");


            // ابتدا MessageSend ذخیره می‌شود تا ID داشته باشیم
            await base.AddAsync(entity, cancellationToken, true);

            try
            {
                var result = await SendBulkAsync(entity.Message,phone, entity.DateTimeSend);

                var messageLog = new MessageLog
                {
                    MessageSendID = entity.ID,
                    ActionDateTime = DateTime.Now,

                    SendStatusID = result.Status
                        ? (byte)Common.SendStatusType.API_OK
                        : (byte)Common.SendStatusType.SendAgain,

                    StatusCodeReturn = result.StatusCode.ToString(),

                    SendActive = true,
                    IsComplete = result.Status,

                    Description = result.Status
                        ? "پیامک با موفقیت ارسال شد."
                        : "ارسال پیامک ناموفق بود."
                };

                await _messageLogRepository.AddAsync(messageLog, cancellationToken);
            }
            catch (Exception ex)
            {
                var messageLog = new MessageLog
                {
                    MessageSendID = entity.ID,
                    ActionDateTime = DateTime.Now,

                    // طبق بیزینس شما
                    SendStatusID =
                        (byte)Common.SendStatusType.SendAgain,

                    StatusCodeReturn = "EXCEPTION",

                    SendActive = true,
                    IsComplete = false,

                    Description = ex.Message
                };

                await _messageLogRepository.AddAsync(messageLog,cancellationToken);

                // مهم:
                // Controller باید بفهمد این شماره Fail شده
                throw;
            }





        }





        public override void Add(MessageSend entity, bool saveNow = true)
        {
            entity.InsertDateTime = DateTime.Now;
            base.Add(entity, saveNow);

            string phone = entity.PhoneNummber.Nummber;

            var res = SendBulkAsync(entity.Message, entity.PhoneNummber.Nummber, entity.DateTimeSend);


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
