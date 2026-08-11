using DAL.Repository;
using DAL.Repository.Basic.Personal;
using Entities.Base;
using Entities.Basic.Personel;
using Microsoft.AspNetCore.Authorization;
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

        private readonly PersonGroupRepository  _personGroupRepository;

        public PersonGroupController(IRepository<PersonGroup> repository, PersonGroupRepository personGroupRepository): base(repository)
        {
            _personGroupRepository =    personGroupRepository;
        }



        [Authorize]
        [HttpGet("SearchMembers")]
        public async Task<IActionResult> SearchMembers( [FromQuery] long groupID, [FromQuery] string? personCode,
            [FromQuery] string? name, [FromQuery] int page = 1, [FromQuery] int pageSize = 15,CancellationToken cancellationToken = default)
        {
            if (groupID <= 0)
            {
                return BadRequest("شناسه گروه معتبر نیست.");
            }

            var result =
                await _personGroupRepository
                    .SearchMembersAsync(
                        groupID,
                        personCode,
                        name,
                        page,
                        pageSize,
                        cancellationToken);

            return Ok(result);
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


        [HttpPost]
        [Route("Create")]
        public override async Task<IActionResult> Create(PersonGroup instance, CancellationToken cancellationToken)
        {
            var exists = await _personGroupRepository.ExistsAsync(instance.PersonID,instance.GroupID,cancellationToken);

            if (exists)
            {
                return Conflict(new{ message = "این پرسنل قبلاً عضو این گروه است." });
            }

            await _repository.AddAsync(instance, cancellationToken);

            return Ok(instance);

           
        }



    }
}
