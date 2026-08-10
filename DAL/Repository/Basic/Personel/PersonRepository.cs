

using Entities.Basic.Personel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repository.Basic.Personal
{
    public class PersonRepository : Repository<Person>
    {



        public PersonRepository(AppDbContext dbContext) : base(dbContext)
        {
  
        }

        public Task<Person> GetByID(long id, CancellationToken cancellationToken)
        {

            return Table.Where(w => w.ID == id).SingleOrDefaultAsync(cancellationToken);
        }

        public Task<Person> GetByPersonCode(string personCode, CancellationToken cancellationToken)
        {
            return Table.Where(w => w.PersonCode == personCode).SingleOrDefaultAsync(cancellationToken);
        }

        

    }
}
