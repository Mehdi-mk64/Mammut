using DAL.Repository;
using Entities.Basic.Personel;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using SystemManagment.Controller.Base;

namespace SystemManagment.Controller
{
    public class PersonUnitController : ApiControllerBase<Entities.Basic.Personel.PersonUnit>
    {
        public PersonUnitController(IRepository<PersonUnit> repository) : base(repository)
        {
        }
        public override Task<IActionResult> Create(PersonUnit instance, CancellationToken cancellationToken)
        {
           
            return base.Create(instance, cancellationToken);
        }
    }
}

