using DAL.Repository;
using Entities.Basic.Personel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SystemManagment.Controller.Base;

namespace SMSAPI.Controller.Person
{
    public class PhoneNumberController : ApiControllerBase<Entities.Basic.Personel.PhonNumbers>
    {
        public PhoneNumberController(IRepository<PhonNumbers> repository) : base(repository)
        {
        }
        


    }
}
