using DAL.Repository;
using Entities.Basic.Personel;
using Entities.Basic.SMS;
using SystemManagment.Controller.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace SMSAPI.Controller.SMS
{
    
    public class MessageLogController : ApiControllerBase<Entities.Basic.SMS.MessageLog>
    {
        public MessageLogController(IRepository<MessageLog> repository) : base(repository)
        {
        }
    }
}
