using DAL.Repository;
using Entities.Basic.Personel;
using Entities.Basic.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;



namespace SystemManagment.Controller.Base.Security
{
    public class AccesseGroupController : ApiControllerBase<Entities.Basic.Personel.PersonGroup>
    {
        public AccesseGroupController(IRepository<PersonGroup> repository) : base(repository)
        {
        }


        [HttpGet]
        [Route("AccessGroup/{personID}")]
        public async Task<IActionResult> GetAccessGroupByID(long personID, CancellationToken cancellationToken)
        {
            var res = await _repository.TableNoTracking
                .Where(w => w.PersonID == personID)
                .Select(s => s.PersonGroup_Group)
                .ToListAsync(cancellationToken);

            return Ok(res);
        }


        [Authorize]
        [HttpGet]
        [Route("GetListGroup}")]
        public async Task<IActionResult> GetListGroup(CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized();

            if (!int.TryParse(userIdClaim, out int userID))
                return Unauthorized();

             var listHasAccess = await _repository.TableNoTrackingOf<AccesseGroup>()
                .AnyAsync(x => x.UserID == userID );
            
            return Ok(listHasAccess);
        }

    }
}
