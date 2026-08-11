using DAL.Repository;
using Entities.Base;
using Entities.Basic.Personel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        [HttpGet]
        [Route("PhoneListByGroupID/{groupID}")]
        public async Task<IActionResult> GetPhoneListByGroupID(long groupID, CancellationToken cancellationToken)
        {
            var res = await _repository.TableNoTracking
                .Where(w => w.GroupID == groupID )
                .Select(s => s.PersonGroup_Person.Person_PhoneNumbers)
                .ToListAsync(cancellationToken);
            return Ok(res);
        }

    }
}
