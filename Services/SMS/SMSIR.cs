using Common.Exeptions;
using Common.Utilities;
using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;
using IPE.SmsIrClient.Models.Results;
using RestSharp;
using RestSharp.Authenticators;
using Spire.Pdf.Exporting.XPS.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Services.SMS
{
    public  class SMSIRService
    {

        public static async Task<Entities.DTO.SmsIrResault> SendBulkAsync(string message, string phone, DateTime? sendDateTime = null)
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
                long unixTime =
                    new DateTimeOffset(sendDateTime.Value)
                        .ToUnixTimeSeconds();

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




        //public static async Task<List<Entities.DTO.SmsIrResault>> SendBulkAsync(string apiKey, string message, string[] phons, string? sendDateTime = null)
        //{
        //    SmsIr smsIr = new SmsIr(apiKey);
        //    long lineNumber = 10004501;
        //    string messageText = message;
        //    string[] mobiles = phons.ToArray();

        //    int? sendDateTimeservice = null;
        //    DateTime.TryParse(sendDateTime, out DateTime inputSendDattime);
        //    if (inputSendDattime == DateTime.MinValue)
        //    {
        //        long unixTime = new DateTimeOffset(inputSendDattime).ToUnixTimeSeconds();
        //        sendDateTimeservice = (int)unixTime;
        //    }


        //    var response = await smsIr.BulkSendAsync(lineNumber, messageText, mobiles, sendDateTimeservice);
        //    SendResult sendResult = response.Data;
        //    List<Entities.DTO.SmsIrResault> smsIrResault = new List<Entities.DTO.SmsIrResault>();
        //    byte statusCode = response.Status;
        //    for (int i = 0;i < sendResult.MessageIds.Length;i++)
        //    {
        //        smsIrResault.Add(new Entities.DTO.SmsIrResault
        //        {
        //            MessageId = sendResult.MessageIds[i].GetValueOrDefault(0),
        //            Phone = phons[i],
        //            Status = statusCode == 1 ? true : false,
        //            StatusCode=statusCode
        //        });
        //    }
        //    return smsIrResault;

        //}

    }
}

