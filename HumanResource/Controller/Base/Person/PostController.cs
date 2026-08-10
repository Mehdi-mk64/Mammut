using DAL.Repository;
using Entities.Basic.Personel;
using SystemManagment.Controller.Base;

namespace SystemManagment.Controller
{
    public class PostController : ApiControllerBase<Entities.Basic.Personel.Post>
    {
        public PostController(IRepository<Post> repository) : base(repository)
        {
        }
    }
}

