
using Entities.Basic.Personel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repository.Basic.Personal

{
    public class PersonUnitRepository : Repository<PersonUnit>
    {
        public PersonUnitRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


    }
}
