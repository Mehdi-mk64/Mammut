using Common;
using DAL;
using DAL.Repository.Basic.SMS;
using Entities.Basic.SMS;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.SMS
{
    public class SMSSend
    {
        private AppDbContext dbContext;
        private MessageLogRepository _messageLogRepository;
        public SMSSend()
        {

            dbContext = new AppDbContext();
            _messageLogRepository = new MessageLogRepository(dbContext);
        }
        public async Task MagfaSendSMS()
        {
            //CancellationToken cancellationToken = new CancellationToken();

            //var smsSendList = _messageLogRepository.GetSmsSendListWithApi(cancellationToken).Result;
            //foreach (MessageLog messageLog in smsSendList)
            //{
            //    MessageLog sendMessageLog = new MessageLog()
            //    {

            //        MessageSendID = messageLog.MessageSendID,
            //        ActionDateTime = DateTime.Now,
            //        StatusCodeReturn = "No Impliment"

            //    };
            //    if (messageLog.MessageLog_MessageSend.SmsProviderID == null)
            //    {
            //        sendMessageLog.Description = "SMS ProviderID is Null";
            //        if (messageLog.MessageLog_MessageSend.SendImportanceID == (byte)(SendImportanceType.Important))
            //        {
            //            sendMessageLog.SendActive = true;
            //            sendMessageLog.SendStatusID = (byte)SendStatusType.SendGSM;
            //        }
            //        else
            //        {
            //            sendMessageLog.SendActive = false;
            //            sendMessageLog.SendStatusID = (byte)SendStatusType.Fault;
            //        }
            //        sendMessageLog.IsComplete = false;
            //    }
            //    else
            //    {
            //        var response = SMSIR.MagfaSendSMS(messageLog.MessageLog_MessageSend);
            //        sendMessageLog.Description = response.Content;
            //        if (response.Content.ToString() != null)
            //        {
            //            sendMessageLog.StatusCodeReturn = (string)JObject.Parse(response.Content.ToString())["status"];
            //        }
            //        else
            //        {
            //            sendMessageLog.StatusCodeReturn = "No Response";
            //        }
            //        if (sendMessageLog.StatusCodeReturn == "0")
            //        {
            //            sendMessageLog.SendStatusID = (byte)SendStatusType.API_OK;
            //            sendMessageLog.IsComplete = true;
            //            sendMessageLog.SendActive = false;
            //        }
            //        else 
            //        {
            //            int countSend = _messageLogRepository.GetMessageStatusListByID(messageLog.MessageSendID, SendStatusType.SendAgain, cancellationToken).Result.Count() + 1;

            //            if (messageLog.MessageLog_MessageSend.MaximumTrySendSMS == 0)
            //            {
            //                sendMessageLog.SendStatusID = (byte)SendStatusType.SendAgain;
            //                sendMessageLog.IsComplete = false;
            //                sendMessageLog.SendActive = true;
            //            }

            //            else
            //            {
            //                if (countSend < messageLog.MessageLog_MessageSend.MaximumTrySendSMS)
            //                {
            //                    sendMessageLog.SendStatusID = (byte)SendStatusType.SendAgain;
            //                    sendMessageLog.IsComplete = false;
            //                    sendMessageLog.SendActive = true;

            //                }
            //                else
            //                {
            //                    if (messageLog.MessageLog_MessageSend.SendImportanceID == (byte)SendImportanceType.Important)
            //                    {
            //                        sendMessageLog.SendStatusID = (byte)SendStatusType.SendGSM;
            //                        sendMessageLog.IsComplete = false;
            //                        sendMessageLog.SendActive = true;
            //                    }
            //                    else
            //                    {
            //                        sendMessageLog.SendStatusID = (byte)SendStatusType.Fault;
            //                        sendMessageLog.IsComplete = false;
            //                        sendMessageLog.SendActive = false;
            //                    }
            //                }

            //            }

            //        }


            //    }

            //    await _messageLogRepository.AddAsync(sendMessageLog, cancellationToken);
            //    messageLog.SendActive = false;

            //}
            //if (smsSendList.Count > 0)
            //{
            //    await _messageLogRepository.UpdateRangeAsync(smsSendList, cancellationToken);
            //}

        }







        public async Task GsmSendSMS()
        {
            //CancellationToken cancellationToken = new CancellationToken();

            //var smsSendList = _messageLogRepository.GetSmsSendListWithGSM(cancellationToken).Result;

            //foreach (MessageLog messageLog in smsSendList)
            //{
            //    MessageLog sendMessageLog = new MessageLog()
            //    {

            //        MessageSendID = messageLog.MessageSendID,
            //        ActionDateTime = DateTime.Now
                    
            //    };

            //    if (messageLog.MessageLog_MessageSend.GSMSenderID ==null) 
            //    {
            //        sendMessageLog.Description = "GSMSender Is Null";
            //        sendMessageLog.SendStatusID = (byte)SendStatusType.Fault;
            //        sendMessageLog.IsComplete = true;
            //        sendMessageLog.SendActive = false;
            //        _messageLogRepository.Add(sendMessageLog);
            //        messageLog.SendActive = false;
            //    }
            //    else 
            //    {
            //        var response = GSMService.GSMSendSMS(messageLog.MessageLog_MessageSend);

            //        sendMessageLog.Description = response.Result.ToString();
            //        sendMessageLog.StatusCodeReturn = response.Result.StatusCode.ToString();
            //        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            sendMessageLog.SendStatusID = (byte)SendStatusType.GSM_OK;
            //        }
            //        else
            //        {
            //            sendMessageLog.SendStatusID = (byte)SendStatusType.Fault;
            //        }
            //        sendMessageLog.IsComplete = true;
            //        sendMessageLog.SendActive = false;
            //    }
            //    _messageLogRepository.Add(sendMessageLog);
            //    messageLog.SendActive = false;

            //}
            //if (smsSendList.Count>0 )
            //{
            //    await _messageLogRepository.UpdateRangeAsync(smsSendList, cancellationToken);
            //}
        }
    }






}
