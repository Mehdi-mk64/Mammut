
using Entities.Basic.Personel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repository.Basic.Personal

{
    public class PersonGroupRepository : Repository<PersonGroup>
    {
        public PersonGroupRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


    }
}
