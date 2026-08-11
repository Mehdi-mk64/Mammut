using DAL.Repository;
using DAL.Repository.Basic.Personal;
using DAL.Repository.Basic.Security;
using Entities.Basic.Personel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SystemManagment.Controller.Base.Security
{
    public class AccesseGroupController
        : ApiControllerBase<PersonGroup>
    {
        private readonly AccesseGroupRepository _accesseGroupRepository;

        public AccesseGroupController(
            IRepository<PersonGroup> repository,
            AccesseGroupRepository accesseGroupRepository)
            : base(repository)
        {
            _accesseGroupRepository = accesseGroupRepository;
        }


        [Authorize]
        [HttpGet]
        [Route("GetListGroup")]
        public async Task<IActionResult> GetListGroup(
            CancellationToken cancellationToken)
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
    }
}