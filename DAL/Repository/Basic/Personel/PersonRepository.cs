

using Entities.Basic.Personel;
using Entities.DTO;
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


        public async Task<List<PersonSearchDto>> SearchAsync(string term, int pageSize,  CancellationToken cancellationToken)
        {
            if (pageSize <= 0)
                pageSize = 10;

            if (pageSize > 20)
                pageSize = 20;

            term = term?.Trim() ?? "";

            var query = TableNoTracking   .Where(x => x.IsActive);

            if (!string.IsNullOrWhiteSpace(term))
            {
                query = query.Where(x => x.PersonCode.Contains(term) || x.FirstName.Contains(term) || x.LastName.Contains(term) ||
                    (x.FirstName + " " + x.LastName).Contains(term));
            }

            return await query
                .OrderBy(x => x.PersonCode)
                .Take(pageSize)
                .Select(x => new PersonSearchDto
                {
                    ID = x.ID,
                    PersonCode = x.PersonCode,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    FullName = x.FirstName + " " + x.LastName
                })
                .ToListAsync(cancellationToken);
        }


    }
}
