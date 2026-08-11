using DAL.Repository;
using DAL.Repository.Basic.Personal;
using DAL.Repository.Basic.Security;
using Entities.Basic.Personel;
using Entities.Basic.Security;
using Entities.Basic.SMS;
using Entities.DTO;
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

        private readonly AccesseGroupRepository _accesseGroupRepository;
        private readonly GroupRepository _groupRepository;

        public MessageSendController(
            IRepository<MessageSend> repository,
            AccesseGroupRepository accesseGroupRepository,
            GroupRepository groupRepository)
            : base(repository)
        {
            _accesseGroupRepository = accesseGroupRepository;
            _groupRepository = groupRepository;
        }



        [Authorize]
        [HttpPost("SendToGroup")]
        public async Task<IActionResult> SendToGroup([FromBody] SendToGroupDto model, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized();

            if (!long.TryParse(userIdClaim, out long userID))
                return Unauthorized();


            var hasAccess =  await _accesseGroupRepository.HasAccessToGroupAsync(userID, model.GroupID, cancellationToken);

            if (!hasAccess)
                return Forbid();


            var phoneNumbers =
                await _groupRepository
                    .GetPhoneNumbersByGroupIDAsync(model.GroupID,cancellationToken);

            if (phoneNumbers == null || !phoneNumbers.Any())
                return NotFound(
                    "هیچ شماره تلفنی برای این گروه پیدا نشد.");


            foreach (var phone in phoneNumbers)
            {
                var messageSend = new MessageSend
                {
                    Message = model.Message,

                    PhoneNumberID = phone.PhoneNumberID,

                    DateTimeSend = model.DateTimeSend,

                    SmsProviderID = model.SmsProviderID,

                    SendImportanceID =  model.SendImportanceID
                };

                await _repository.AddAsync(messageSend, cancellationToken);
            }


            return Ok(new
            {
                Message = "ارسال پیامک انجام شد.",
                Count = phoneNumbers.Count
            });
        }



        //[Authorize]
        //[HttpPost("SendToGroup")]
        //public async Task<IActionResult> SendToGroup([FromBody] SendToGroupDto model, CancellationToken cancellationToken)
        //{
        //    var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    if (string.IsNullOrWhiteSpace(userIdClaim))
        //        return Unauthorized();

        //    if (!int.TryParse(userIdClaim, out int userID))
        //        return Unauthorized();

        //    var hasAccess = await _repository.TableNoTrackingOf<AccesseGroup>()
        //        .AnyAsync(x => x.UserID == userID && x.GroupID == model.GroupID);

        //    if (!hasAccess)
        //        return Forbid();


        //    var personTels = await _repository
        //        .TableNoTrackingOf<PersonGroup>()
        //        .Where(x => x.GroupID == model.GroupID)
        //        .Select(x => new
        //        {
        //            x.PersonID
        //        }).Join(_repository.TableNoTrackingOf<PhonNumbers>(), p => p.PersonID, t => t.PersonID, (per, tel) => new { PersonId =per.PersonID , TelNumme=tel.Nummber,telID=tel.ID})
        //        .ToListAsync();



        //    if (!personTels.Any())
        //        return NotFound("No person found in this group.");


        //    MessageSend messageSend = new MessageSend();
        //    foreach (var person in personTels)
        //    {
        //        messageSend.Message = model.Message;
        //        messageSend.PhoneNumberID = person.telID;
        //        messageSend.DateTimeSend = model.DateTimeSend;
        //        messageSend.SmsProviderID =model.SmsProviderID;
        //        messageSend.SendImportanceID =model.SendImportanceID;
        //        await _repository.AddAsync(messageSend, cancellationToken);

        //    }

        //    return Ok();
        //}




    }
}
