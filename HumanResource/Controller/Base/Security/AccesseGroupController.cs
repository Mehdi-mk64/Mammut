using DAL.Repository;
using DAL.Repository.Basic.Personal;
using DAL.Repository.Basic.Security;
using Entities.Basic.Personel;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SystemManagment.Controller.Base.Security
{
    public class AccesseGroupController : ControllerBase
    {
        private readonly AccesseGroupRepository _accesseGroupRepository;

        public AccesseGroupController( IRepository<PersonGroup> repository, AccesseGroupRepository accesseGroupRepository)
            
        {
            _accesseGroupRepository = accesseGroupRepository;
        }


        [Authorize]
        [HttpGet]
        [Route("GetListGroup")]
        public async Task<IActionResult> GetListGroup( CancellationToken cancellationToken)
        {
            var userIdClaim =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized();

            if (!int.TryParse(userIdClaim, out int userID))
                return Unauthorized();

            var groups =
                await _accesseGroupRepository
                    .GetUserGroupsAsync(
                        userID,
                        cancellationToken);

            return Ok(groups);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("UserGroupIds/{userID:int}")]
        public async Task<IActionResult> GetUserGroupIds( int userID, CancellationToken cancellationToken)
        {
            var groupIDs = await _accesseGroupRepository
                    .GetUserGroupIdsAsync(
                        userID,
                        cancellationToken);

            return Ok(groupIDs);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Grant")]
        public async Task<IActionResult> Grant( AccessGroupDto model, CancellationToken cancellationToken)
        {
            var added = await _accesseGroupRepository
                    .GrantAsync(
                        model.UserID,
                        model.GroupID,
                        cancellationToken);

            if (!added)
            {
                return Conflict(new { message = "این کاربر از قبل به این گروه دسترسی دارد." });
            }

            return Ok(new { message = "دسترسی با موفقیت اضافه شد." });
        }



        [Authorize(Roles = "Admin")]
        [HttpDelete("Revoke")]
        public async Task<IActionResult> Revoke( [FromQuery] int userID, [FromQuery] long groupID, CancellationToken cancellationToken)
        {
            var removed = await _accesseGroupRepository
                    .RevokeAsync(
                        userID,
                        groupID,
                        cancellationToken);

            if (!removed)
            {
                return NotFound(new { message = "این کاربر به این گروه دسترسی ندارد." });
            }

            return Ok(new { message = "دسترسی با موفقیت حذف شد." });
        }

    }
}