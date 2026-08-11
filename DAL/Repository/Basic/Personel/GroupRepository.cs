using Entities.Basic.Personel;
using Entities.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repository.Basic.Personal
{
    public class GroupRepository : Repository<Group>
    {
        public GroupRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }
        public async Task<List<Entities.DTO.GroupPhoneDto>>
            GetPhoneNumbersByGroupIDAsync(
                long groupID,
                CancellationToken cancellationToken)
        {
            return await TableNoTrackingOf<PersonGroup>()
                .Where(pg =>
                    pg.GroupID == groupID &&
                    pg.PersonGroup_Person.IsActive)
                .SelectMany(pg =>
                    pg.PersonGroup_Person.Person_PhoneNumbers)
                .Select(phone => new Entities.DTO.GroupPhoneDto
                {
                    PhoneNumberID = phone.ID,
                    PhoneNumber = phone.Nummber
                })
                .Distinct()
                .ToListAsync(cancellationToken);
        }



        public async Task<PagedResultDto<GroupSearchDto>> SearchAsync( string? title, int page, int pageSize, CancellationToken cancellationToken)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 15;

            if (pageSize > 50)
                pageSize = 50;

            var query = TableNoTracking.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                title = title.Trim();

                query = query.Where(x => x.Title.Contains(title));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(x => x.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new GroupSearchDto
                {
                    ID = x.ID,
                    Title = x.Title
                })
                .ToListAsync(cancellationToken);

            return new PagedResultDto<GroupSearchDto>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }






    }
}