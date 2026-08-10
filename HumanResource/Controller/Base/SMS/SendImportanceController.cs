using DAL.Repository;
using Entities.Basic.Personel;
using Entities.Basic.SMS;
using SystemManagment.Controller.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace SMSAPI.Controller.SMS
{
    public class SendImportanceController : ApiControllerBase<SendImportance>
    {
        public SendImportanceController(IRepository<SendImportance> repository) : base(repository)
        {
        }
    }
}
