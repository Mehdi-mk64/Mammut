using DAL.Repository;
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
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ApiControllerBase<Entities.Basic.Personel.Group>
    {
        public GroupController(IRepository<Group> repository) : base(repository)
        {
        }

        [Authorize]
     
        [HttpGet("GetIdByName")]
        public async Task<IActionResult> GetIDByName([FromQuery] string nameGroup, CancellationToken cancellationToken)
        {
            var res = await _repository.TableNoTracking
                .Where(w => w.Title == nameGroup)
                .Select(s => s.ID)
                .ToListAsync(cancellationToken);

            return Ok(res);
        }
    }
}

