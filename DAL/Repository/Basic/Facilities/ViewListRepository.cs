using Common;
using DAL.Repository.Basic.Personal;
using DAL.Repository.Basic.SMS;
using Entities.Base;
using Entities.Basic.Facilities;
using Entities.Basic.Personel;
using Entities.Basic.SMS;
using Entities.Basic.ViewModel;
using Microsoft.EntityFrameworkCore;
using Spire.Pdf.OPC;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repository.Basic.Facilities
{
    public class ViewListRepository : Repository<ViewList>
    {
        #region Fields
        private SendProviderRepository _sendProviderRepository;
        private PhonNummberRepository _phonNummberRepository;
        private MessageSendRepository _messageSendRepository;
        private ViewModelMessageRepository _viewModelMessageRepository;


        #endregion

        #region Constractor
        public ViewListRepository(AppDbContext dbContext) : base(dbContext)
        {
            //_gsmSenderRepository = new GSMSenderRepository(dbContext);
            _sendProviderRepository = new SendProviderRepository(dbContext);
            _phonNummberRepository = new PhonNummberRepository(dbContext);
            _messageSendRepository = new MessageSendRepository(dbContext);
            _viewModelMessageRepository = new ViewModelMessageRepository(dbContext);
        }
        #endregion

        //private async Task AddViewDataToDB(ViewModelMessage message, CancellationToken cancellationToken)
        //{
        //    MessageSend messageSend = new MessageSend();
           
        //    messageSend.Message = message.Message;
        //    messageSend.MaximumTrySendSMS = message.MaximumTrySendSMS;
        //    messageSend.DateSend = message.DateSend ?? DateTime.Now;
        //    messageSend.TimeSend = message.TimeSend ?? DateTime.Now.TimeOfDay;
        //    messageSend.InsertDateTime = DateTime.Now;
        //    messageSend.SendImportanceID = (message.Importance ? (byte)(SendImportanceType.Important) : (message.OnlyGSMSend ? (byte)(SendImportanceType.ForceGSM) : (byte)(SendImportanceType.Normal) ));
        //    messageSend.GSMSenderID =(message.GSMSenderTitle==null?0: _gsmSenderRepository.GetByName(message.GSMSenderTitle)?.ID??0) ;
        //    messageSend.SmsProviderID = (message.SmsProviderTitle==null?0: _sendProviderRepository.GetByName(message.SmsProviderTitle)?.ID??0);
        //    if (messageSend.Message==null) 
        //    {
        //        message.MessageSendID = null;
        //        message.HasError = true;
        //        message.IsComlpete = false;
        //        message.MessageSendID = null;
        //        return;
        //    }
        //    else if (messageSend.SendImportanceID == (byte)SendImportanceType.Important &&(messageSend.SmsProviderID == 0 || messageSend.GSMSenderID == 0 ))
        //    {
        //        message.MessageSendID = null;
        //        message.HasError = true;
        //        message.IsComlpete = false;
        //        message.MessageSendID = null;
        //        return;
        //    }
        //    else if (messageSend.SendImportanceID == (byte)SendImportanceType.ForceGSM && messageSend.GSMSenderID == 0)
        //    {
        //        message.MessageSendID = null;
        //        message.HasError = true;
        //        message.IsComlpete = false;
        //        message.MessageSendID = null;
        //        return;
        //    }
        //    else if (messageSend.SendImportanceID == (byte)SendImportanceType.Normal && messageSend.SmsProviderID == 0 )
        //    {
        //        message.MessageSendID= null;
        //        message.HasError = true;
        //        message.IsComlpete = false;
        //        message.MessageSendID = null;
        //        return;
        //    }
        


        //    PhonNumbers phonNumbers = new PhonNumbers();
        //    phonNumbers = _phonNummberRepository.GetByNumber(message.PhoneNumber);
        //    if (phonNumbers != null)
        //    {
        //        messageSend.PhoneNumberID = phonNumbers.ID;
        //    }
        //    else
        //    {
        //        if (message.AddAnonymous)
        //        {
        //            await _phonNummberRepository.AddAsync(new PhonNumbers() { Nummber = message.PhoneNumber, PersonID = 1 }, cancellationToken);
        //            //phonNumbers = _phonNummberRepository.GetByNumber(message.PhoneNumber);
        //            messageSend.PhoneNumberID = _phonNummberRepository.GetByNumber(message.PhoneNumber).ID;
        //        }
        //        else
        //        {
        //           message.MessageSendID = null;
        //           message.HasError = true;
        //           message.IsComlpete = false;
        //           message.MessageSendID = null;
        //           return;
        //        }
        //    }

        //    try
        //    {
        //        messageSend.SmsProviderID = (messageSend.SmsProviderID == 0 ? null : messageSend.SmsProviderID);
        //        messageSend.GSMSenderID = (messageSend.GSMSenderID == 0 ? null : messageSend.GSMSenderID);
        //        _messageSendRepository.Add(messageSend);
        //        message.HasError = false;
        //        message.IsComlpete = true;
        //        message.MessageSendID = messageSend.ID;
        //    }
        //    catch (Exception ex)
        //    {
        //        message.MessageSendID = null;
        //        message.HasError = true;
        //        message.IsComlpete = false;
        //        message.MessageSendID = null;
        //    }

        //}

        public async Task InsertDataFromView(CancellationToken cancellationToken)
        {

            try
            {
                List<ViewList> viewList = await Table.ToListAsync<ViewList>();
                foreach (var view in viewList)
                {
                    var ListData = _viewModelMessageRepository.GetDataList(view);
                    foreach (ViewModelMessage msgdata in ListData)
                    {
                      //  await AddViewDataToDB(msgdata, cancellationToken);
                        _viewModelMessageRepository.UpdateDataList(msgdata, view);
                    }
 
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            


        }




    }
}
