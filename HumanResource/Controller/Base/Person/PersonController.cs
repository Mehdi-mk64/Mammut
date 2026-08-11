using DAL.Repository;
using DAL.Repository.Basic.Personal;
using DAL.Repository.Basic.SMS;
using Entities.Basic.Personel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using SystemManagment.Controller.Base;

namespace SMSAPI.Controller.Person
{
    public class PersonController : ApiControllerBase<Entities.Basic.Personel.Person>
    {
        private readonly PersonRepository _personRepository;

        public PersonController(IRepository<Entities.Basic.Personel.Person> repository, PersonRepository personRepository): base(repository)
        {
            _personRepository = personRepository;
        }
  

        [Authorize]
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string term, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _personRepository.SearchAsync(term,pageSize,cancellationToken);

            return Ok(result);
        }





    }
}
