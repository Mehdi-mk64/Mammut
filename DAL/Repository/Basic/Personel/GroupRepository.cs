using Entities.Basic.Personel;
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

        public async Task<List<string>> GetPhoneNumbersByGroupIDAsync(
            long groupID,
            CancellationToken cancellationToken)
        {
            return await DbContext.PersonelGroup
                .AsNoTracking()
                .Where(pg =>
                    pg.GroupID == groupID &&
                    pg.PersonGroup_Person.IsActive)
                .SelectMany(pg =>
                    pg.PersonGroup_Person.Person_PhoneNumbers)
                .Select(phone => phone.Nummber)
                .Distinct()
                .ToListAsync(cancellationToken);
        }
    }
}