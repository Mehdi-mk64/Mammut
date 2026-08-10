
using Entities.Basic.Personel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repository.Basic.Personal

{
    public class PersonPostRepository : Repository<PersonPost>
    {
        public PersonPostRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


    }
}
