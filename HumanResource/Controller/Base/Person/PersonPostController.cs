using DAL.Repository;
using Entities.Basic.Personel;
using SystemManagment.Controller.Base;

namespace SystemManagment.Controller
{
    public class PersonPostController : ApiControllerBase<Entities.Basic.Personel.PersonPost>
    {
        public PersonPostController(IRepository<PersonPost> repository) : base(repository)
        {
        }

         
    }
}

