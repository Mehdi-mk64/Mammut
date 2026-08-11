using DAL.Repository;
using DAL.Repository.Base;
using DAL.Repository.Basic.Personal;
using Entities.Basic.Personel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SystemManagment.Controller.Base;

namespace SystemManagment.Controller
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ApiControllerBase<Entities.Basic.Personel.Group>
    {
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupRepository repository) : base(repository)
        {

            _groupRepository = repository;
        }


        [HttpGet("GetIdByName")]
        public async Task<IActionResult> GetIDByName([FromQuery] string nameGroup, CancellationToken cancellationToken)
        {
            var res = await _repository.TableNoTracking
                .Where(w => w.Title == nameGroup)
                .Select(s => s.ID)
                .ToListAsync(cancellationToken);

            return Ok(res);
        }


        [HttpGet("GetPhoneNumbersByGroupID")]
        public async Task<IActionResult> GetPhoneNumbersByGroupID(
        [FromQuery] long groupID,
        CancellationToken cancellationToken)
        {
            var phoneNumbers = await _groupRepository.GetPhoneNumbersByGroupIDAsync( groupID, cancellationToken);

            return Ok(phoneNumbers);
        }





    }
}

