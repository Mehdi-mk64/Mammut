using DAL.Repository;
using Entities.Basic.Personel;
using SystemManagment.Controller.Base;

namespace SystemManagment.Controller
{
    public class GroupController : ApiControllerBase<Entities.Basic.Personel.Group>
    {
        public GroupController(IRepository<Group> repository) : base(repository)
        {
        }
    }
}

