using DAL.Repository;
using Entities.Basic.Personel;
using Entities.Basic.SMS;
using SystemManagment.Controller.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace SMSAPI.Controller.SMS
{
    public class SendStatusController : ApiControllerBase<Entities.Basic.SMS.SendStatus>
    {
        public SendStatusController(IRepository<SendStatus> repository) : base(repository)
        {
        }
    }
}
