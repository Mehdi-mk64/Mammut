using DAL.Repository;
using DAL.Repository.Base;
using DAL.Repository.Basic.Personal;
using DAL.Repository.Basic.Security;
using Entities.Basic.Personel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
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
        private readonly GroupRepository _groupRepository;
        private readonly AccesseGroupRepository _accesseGroupRepository;


        public GroupController(IRepository<Group> repository, GroupRepository groupRepository , AccesseGroupRepository accesseGroupRepository) : base(repository)
        {
            _groupRepository = groupRepository;
            _accesseGroupRepository = accesseGroupRepository;

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


        [Authorize]
        [HttpGet("GetPhoneNumbersByGroupID")]
        public async Task<IActionResult> GetPhoneNumbersByGroupID([FromQuery] long groupID,CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized();

            if (!long.TryParse(userIdClaim, out long userID))
                return Unauthorized();

            var hasAccess = await _accesseGroupRepository
                    .HasAccessToGroupAsync(
                        userID,
                        groupID,
                        cancellationToken);

            if (!hasAccess)
                return Forbid();

            var phoneNumbers = await _groupRepository
                    .GetPhoneNumbersByGroupIDAsync(
                        groupID,
                        cancellationToken);

            return Ok(phoneNumbers);
        }


    }
}

