
using Entities.Basic.Personel;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Entities.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DAL.Repository.Basic.Personal

{
    public class PersonGroupRepository : Repository<PersonGroup>
    {
        public PersonGroupRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedResultDto<PersonGroupSearchDto>>  SearchMembersAsync( long groupID,  string? personCode,
            string? name,int page, int pageSize,CancellationToken cancellationToken)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 15;

            if (pageSize > 50)
                pageSize = 50;

            var query = TableNoTracking.Where(x => x.GroupID == groupID);

            if (!string.IsNullOrWhiteSpace(personCode))
            {
                personCode = personCode.Trim();

                query = query.Where(x => x.PersonGroup_Person.PersonCode.Contains(personCode));
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();

                query = query.Where(x =>
                    x.PersonGroup_Person.FirstName.Contains(name) ||
                    x.PersonGroup_Person.LastName.Contains(name) ||
                    (x.PersonGroup_Person.FirstName + " " +
                     x.PersonGroup_Person.LastName).Contains(name));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query.OrderBy(x => x.PersonGroup_Person.PersonCode)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .Select(x => new PersonGroupSearchDto
                {
                    PersonGroupID = x.ID,
                    PersonID = x.PersonID,
                    PersonCode = x.PersonGroup_Person.PersonCode,
                    FirstName = x.PersonGroup_Person.FirstName,
                    LastName = x.PersonGroup_Person.LastName,
                    FullName = x.PersonGroup_Person.FirstName +" " + x.PersonGroup_Person.LastName
                }).ToListAsync(cancellationToken);

            return new PagedResultDto<PersonGroupSearchDto>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

    }
}
