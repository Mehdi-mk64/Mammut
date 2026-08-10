using DAL.Repository;
using Entities.Basic.Personel;
using SystemManagment.Controller.Base;
using Microsoft.AspNetCore.Mvc;

namespace SMSAPI.Controller.Person
{
    public class GenderController : ApiControllerBase<Entities.Basic.Personel.Gender>
    {
        public GenderController(IRepository<Gender> repository) : base(repository)
        {
        }
    }
}
