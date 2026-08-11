
using DAL.Repository.Base;
using Entities.Base;
using Entities.Basic.Personel;
using Entities.Basic.Security;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace DAL.Repository.Basic.Security

{
    public class AccesseGroupRepository :Repository<AccesseGroup>
    {
        public AccesseGroupRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Group>> GetUserGroupsAsync(long userID,  CancellationToken cancellationToken)
        {
            return await TableNoTracking
                .Where(x => x.UserID == userID)
                .Select(x => x.AccesseGroup_Group)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasAccessToGroupAsync(long userID, long groupID,CancellationToken cancellationToken)
        {
            return await TableNoTracking
                .AnyAsync(
                    x => x.UserID == userID &&
                         x.GroupID == groupID,
                    cancellationToken);
        }

    }
}
