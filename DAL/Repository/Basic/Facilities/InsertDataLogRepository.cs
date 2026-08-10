using Entities.Basic.Facilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Basic.Facilities
{
    public class InsertDataLogRepository : Repository<InsertDataLog>
    {
        public InsertDataLogRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
