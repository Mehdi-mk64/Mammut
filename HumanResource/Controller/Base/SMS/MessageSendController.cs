using DAL.Repository;
using DAL.Repository.Basic.Personal;
using DAL.Repository.Basic.Security;
using DAL.Repository.Basic.SMS;
using Entities.Basic.Personel;
using Entities.Basic.Security;
using Entities.Basic.SMS;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        private readonly MessageSendRepository _messageSendRepository;

        public MessageSendController(IRepository<MessageSend> repository, AccesseGroupRepository accesseGroupRepository,GroupRepository groupRepository, MessageSendRepository messageSendRepository): base(repository)
        {
            _accesseGroupRepository = accesseGroupRepository;
            _groupRepository = groupRepository;
            _messageSendRepository = messageSendRepository;
        }


        [Authorize]
        [HttpPost("SendToGroup")]
        public async Task<IActionResult> SendToGroup([FromBody] SendToGroupDto model, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized();

            if (!int.TryParse(userIdClaim, out int userID))
                return Unauthorized();


            var hasAccess =  await _accesseGroupRepository.HasAccessToGroupAsync(userID, model.GroupID, cancellationToken);

            if (!hasAccess)
                return Forbid();


            var phoneNumbers =
                await _groupRepository.GetPhoneNumbersByGroupIDAsync(model.GroupID,cancellationToken);

            if (phoneNumbers == null || !phoneNumbers.Any())
                return NotFound(
                    "هیچ شماره تلفنی برای این گروه پیدا نشد.");

            int successCount = 0;
            int failedCount = 0;

            var failedPhones = new List<string>();

            foreach (var phone in phoneNumbers)
            {
                try
                {
                    var messageSend = new MessageSend
                    {
                        UserID = userID,
                        Message = model.Message ,
                        PhoneNumberID = phone.PhoneNumberID,
                        DateTimeSend = model.DateTimeSend,
                        SmsProviderID = model.SmsProviderID,
                        SendImportanceID = model.SendImportanceID
                    };

                    var result =  await _messageSendRepository.SendAsync( messageSend, cancellationToken);

                    if (result.Status)
                    {
                        successCount++;
                    }
                    else
                    {
                        failedCount++;
                        failedPhones.Add(phone.PhoneNumber);
                    }

                }
                catch (Exception)
                {
                    failedCount++;

                    failedPhones.Add(phone.PhoneNumber);

                    // عمداً throw نمی‌کنیم
                    // تا شماره بعدی ارسال شود
                }
            }



            return Ok(new
            {
                Message = "عملیات ارسال پیامک پایان یافت.",

                TotalCount = phoneNumbers.Count,

                SuccessCount = successCount,

                FailedCount = failedCount,

                FailedPhones = failedPhones
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
