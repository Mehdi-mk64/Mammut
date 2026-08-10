using Common.Exeptions;
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
    public class SMSIRService
    {


        public static async Task<List<Entities.DTO.SmsIrResault>> SendBulkAsync(string apiKey, string message, List<string> phons, string? sendDateTime = null)
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
            for (int i = 0;i < sendResult.MessageIds.Length;i++)
            {
                smsIrResault.Add(new Entities.DTO.SmsIrResault
                {
                    MessageId = sendResult.MessageIds[i].GetValueOrDefault(0),
                    Phone = phons[i],
                    Status = statusCode == 1 ? true : false,
                    StatusCode=statusCode
                });
            }
            return smsIrResault;
                  
        }

    }
}


    //    public static async Task GetSendReportsAsync()
    //    {
    //        SmsIr smsIr = new SmsIr("YOUR API KEY");

    //        int pageNumber = 1;
    //        int pageSize = 100; // max: 100

    //        var response = await smsIr.GetLiveReportAsync(pageNumber, pageSize);

    //        MessageReportResult[] messages = response.Data;
    //        foreach (var message in messages)
    //        {
    //            int messageId = message.MessageId;
    //            long lineNumber = message.LineNumber;
    //            long mobile = message.Mobile;
    //            string messageText = message.MessageText;
    //            int sendUnixTime = message.SendDateTime;
    //            byte? deliveryState = message.DeliveryState;
    //            int? deliveryUnixTime = message.DeliveryDateTime;
    //            decimal cost = message.Cost;
    //        }
    //    }



    //    }
