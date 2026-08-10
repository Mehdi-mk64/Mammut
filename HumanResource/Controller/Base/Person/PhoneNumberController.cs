using DAL.Repository;
using Entities.Basic.Personel;
using SystemManagment.Controller.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace SMSAPI.Controller.Person
{
    public class PhoneNumberController : ApiControllerBase<Entities.Basic.Personel.PhonNumbers>
    {
        public PhoneNumberController(IRepository<PhonNumbers> repository) : base(repository)
        {
        }
    }
}
