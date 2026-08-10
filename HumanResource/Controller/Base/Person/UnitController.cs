using DAL.Repository;
using Entities.Basic.Personel;
using SystemManagment.Controller.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace SMSAPI.Controller.Person
{
    public class UnitController : ApiControllerBase<Entities.Basic.Personel.Unit>
    {
        public UnitController(IRepository<Unit> repository) : base(repository)
        {
        }

        [NonAction]
        public override Task<IActionResult> Create(Unit instance, CancellationToken cancellationToken)
        {      
            return base.Create(instance, cancellationToken);
        }


        [HttpPost]
        [Route("AddWithParent")]
        public async Task<IActionResult> AddWithParent( Unit model, int? parentUnitID, CancellationToken cancellationToken)
        {
            model.ParentUnitID = parentUnitID;
            await _repository.AddAsync(model, cancellationToken);

            return Ok(model);
        }
    }
}
