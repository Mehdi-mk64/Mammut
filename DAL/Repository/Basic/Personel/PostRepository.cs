
using Entities.Basic.Personel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repository.Basic.Personal

{
    public class PostRepository : Repository<Post>
    {
        public PostRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


    }
}
