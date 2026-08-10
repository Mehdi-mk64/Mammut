using DAL.Repository;
using Entities.Base;
using Entities.Basic.Personel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SystemManagment.Controller.Base;

namespace SMSAPI.Controller.Person
{
    public class PersonGroupController : ApiControllerBase<Entities.Basic.Personel.PersonGroup>
    {
        public PersonGroupController(IRepository<PersonGroup> repository) : base(repository)
        {
        }

    }
}
