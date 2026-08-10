using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Basic.SMS;
using Entities.Base;

namespace Entities.Basic.ViewModel

{
    public class ViewModelMessage : Base.BaseEntities<long>
    {

        #region Properties

        public string Message { get; set; }

        public string PhoneNumber { get; set; }

        public int MaximumTrySendSMS { get; set; }

        public DateTime? DateSend { get; set; }
        public  TimeSpan? TimeSend { get; set; }

        public string SmsProviderTitle  { get; set; }
     
        public string GSMSenderTitle { get; set; }
   
        public bool Importance { get; set; }

        public bool OnlyGSMSend { get; set; }

        public long? MessageSendID { get; set; }

        public bool AddAnonymous { get; set; }

        public bool IsComlpete { get; set; }
        public bool HasError { get; set; }

        public DateTime? DateInsert { get; set; }

        #endregion


        #region Configuration

        public class ViewModelMessageConfiguration : IEntityTypeConfiguration<ViewModelMessage>
        {


            public void Configure(EntityTypeBuilder<ViewModelMessage> builder)
            {
               builder.HasNoKey();
                
            }
        }
        #endregion



    }




}
