
using DAL.Repository.Base;
using Entities.Basic.Personel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace DAL.Repository.Basic.Personal

{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<string>> GetPhoneNumbersByGroupIDAsync(long groupID,  CancellationToken cancellationToken)
        {
            var phoneNumbers = await TableNoTrackingOf<PersonGroup>()
                .Where(pg => pg.GroupID == groupID)
                .SelectMany(pg => pg.PersonGroup_Person.Person_PhoneNumbers)
                .Select(p => p.Nummber)
                .Distinct()
                .ToListAsync(cancellationToken);
            return phoneNumbers;
        }

    }
}
