using Entities.Basic.Personel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repository.Basic.Personal

{
    public class UnitRepository : Repository<Unit>
    {
        public UnitRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
