using DAL.Repository;
using Entities.Basic.Personel;
using Entities.Basic.Security;
using Entities.Basic.SMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using SystemManagment.Controller.Base;

namespace SMSAPI.Controller.SMS
{
    public class MessageSendController : ApiControllerBase<Entities.Basic.SMS.MessageSend>
    {
        public MessageSendController(IRepository<MessageSend> repository) : base(repository)
        {
        }
        public override Task<IActionResult> Create(MessageSend instance, CancellationToken cancellationToken)
        {
            return base.Create(instance, cancellationToken);
        }


        [Authorize]
        [HttpPost("SendToGroup")]
        public async Task<IActionResult> SendToGroup( string message, long groupID, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized();

            if (!int.TryParse(userIdClaim, out int userID))
                return Unauthorized();

            var hasAccess = await _repository.TableNoTrackingOf<AccesseGroup>()
                .AnyAsync(x => x.UserID == userID && x.GroupID == groupID);

            if (!hasAccess)
                return Forbid();


            var personTels = await _repository
                .TableNoTrackingOf<PersonGroup>()
                .Where(x => x.GroupID == groupID)
                .Select(x => new
                {
                    x.PersonID
                }).Join(_repository.TableNoTrackingOf<PhonNumbers>(), p => p.PersonID, t => t.PersonID, (per, tel) => new { PersonId =per.PersonID , TelNumme=tel.Nummber})
                .ToListAsync();





           

            if (!personTels.Any())
                return NotFound("No person found in this group.");


            MessageLog messageLog = new MessageLog();
            await _repository.UpdateAsync<MessageLog>(messageLog,cancellationToken);

            return Ok(new
            {
                Message = message,
                GroupID = groupID,

            });
        }
    }
}
