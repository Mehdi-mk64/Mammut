using Common;
using Common.Utilities;
using DAL;
using DAL.Repository.Basic.SMS;
using Entities.Base;
using Entities.Basic.SMS;
using IPE.SmsIrClient;
using Newtonsoft.Json.Linq;
using Spire.Pdf.Exporting.XPS.Schema;
using System;
using System.Linq;
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
      


        public async Task SmsIrSendSMS()
        {
            CancellationToken cancellationToken = new CancellationToken();
            ConfigManager configManager = new ConfigManager();
            string apiKey = configManager.GetKeyValue("SmsIRService", "Key");
            var smsSendList = _messageLogRepository.GetSmsSendListWithApi(cancellationToken).Result;
            var phones = smsSendList.Select(s => s.MessageLog_MessageSend.PhoneNummber.Nummber).ToArray();


            //var smsSendList= SMSIRService.SendBulkAsync()


            foreach (MessageLog messageLog in smsSendList)
            {
                MessageLog sendMessageLog = new MessageLog()
                {

                    MessageSendID = messageLog.MessageSendID,
                    ActionDateTime = DateTime.Now,
                    StatusCodeReturn = "No Impliment"

                };
                if (messageLog.MessageLog_MessageSend.SmsProviderID == null)
                {
                    sendMessageLog.Description = "SMS ProviderID is Null";
                    sendMessageLog.SendActive = false;
                    sendMessageLog.SendStatusID = (byte)SendStatusType.Fault;
                    sendMessageLog.IsComplete = false;
                }
                else
                {

                    var smsIrResualt = SMSIRService.SendBulkAsync(messageLog.MessageLog_MessageSend.Message, messageLog.MessageLog_MessageSend.PhoneNummber.Nummber, messageLog.MessageLog_MessageSend.DateTimeSend).Result;

                    sendMessageLog.SendStatusID = smsIrResualt.Status == true ? (byte)Common.SendStatusType.API_OK : (byte)Common.SendStatusType.SendAgain;
                    sendMessageLog.StatusCodeReturn = smsIrResualt.StatusCode.ToString();
                    sendMessageLog.Description = "ارسال مجدد";
                 
                    if (smsIrResualt.StatusCode == 0)
                    {
                        sendMessageLog.SendActive = true;
                        sendMessageLog.IsComplete = true;
                        sendMessageLog.SendStatusID = (byte)SendStatusType.API_OK;

                    }

                    else
                    {
                        int countSend = _messageLogRepository.GetMessageStatusListByID(messageLog.MessageSendID, SendStatusType.SendAgain, cancellationToken).Result.Count() + 1;

                        if (messageLog.MessageLog_MessageSend.MaximumTrySendSMS == 0)
                        {
                            sendMessageLog.SendStatusID = (byte)SendStatusType.SendAgain;
                            sendMessageLog.IsComplete = false;
                            sendMessageLog.SendActive = true;
                        }
                        else
                        {
                            if (countSend < messageLog.MessageLog_MessageSend.MaximumTrySendSMS)
                            {
                                sendMessageLog.SendStatusID = (byte)SendStatusType.SendAgain;
                                sendMessageLog.IsComplete = false;
                                sendMessageLog.SendActive = true;

                            }
                            else
                            {
                                sendMessageLog.SendStatusID = (byte)SendStatusType.Fault;
                                sendMessageLog.IsComplete = false;
                                sendMessageLog.SendActive = false;

                            }

                        }

                    }

                }

                await _messageLogRepository.AddAsync(sendMessageLog, cancellationToken);
                messageLog.SendActive = false;

            }
            if (smsSendList.Count > 0)
            {
                await _messageLogRepository.UpdateRangeAsync(smsSendList, cancellationToken);
            }

        }







    }













}
