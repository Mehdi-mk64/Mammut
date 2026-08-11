using DAL.Repository;
using DAL.Repository.Basic.SMS;
using Entities.Basic.Personel;
using Entities.Basic.SMS;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using SystemManagment.Controller.Base;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SMSAPI.Controller.SMS
{
    
    public class MessageLogController : ApiControllerBase<Entities.Basic.SMS.MessageLog>
    {
        private readonly MessageLogRepository _messageLogRepository;

        public MessageLogController( IRepository<MessageLog> repository, MessageLogRepository messageLogRepository) : base(repository)
        {
            _messageLogRepository = messageLogRepository;
        }


        //[HttpGet("Report")]
        //public async Task<IActionResult> Report( [FromQuery] string? phoneNumber, [FromQuery] string? personCode, [FromQuery] int page = 1,
        //    [FromQuery] int pageSize = 15, CancellationToken cancellationToken = default)
        //{
        //    var result = await _messageLogRepository.SearchReportAsync(
        //                phoneNumber,
        //                personCode,
        //                page,
        //                pageSize,
        //                cancellationToken);

        //    return Ok(result);
        //}


        [Authorize]
        [HttpGet("Report")]
        public async Task<IActionResult> Report( [FromQuery] string? phoneNumber, 
            [FromQuery] string? personCode, [FromQuery] int page = 1, [FromQuery] int pageSize = 15, 
            CancellationToken cancellationToken = default)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdClaim, out int userID))
                return Unauthorized();

            var isAdmin = User.IsInRole("Admin");

            var result = await _messageLogRepository.SearchReportAsync(
                    phoneNumber,
                    personCode,
                    page,
                    pageSize,
                    userID,
                    isAdmin,
                    cancellationToken);

            return Ok(result);
        }

    }
}
