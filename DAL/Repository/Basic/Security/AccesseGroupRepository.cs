
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


        public async Task<List<long>> GetUserGroupIdsAsync( int userID, CancellationToken cancellationToken = default)
        {
            return await TableNoTracking
                .Where(x => x.UserID == userID)
                .Select(x => x.GroupID)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> GrantAsync( int userID, long groupID, CancellationToken cancellationToken = default)
        {
            var exists = await TableNoTracking.AnyAsync(
                x => x.UserID == userID &&
                     x.GroupID == groupID,
                cancellationToken);

            if (exists)
                return false;

            var entity = new AccesseGroup { UserID = userID, GroupID = groupID };

            await AddAsync(entity, cancellationToken);

            return true;
        }

        public async Task<bool> RevokeAsync( int userID, long groupID, CancellationToken cancellationToken = default)
        {
            var entity = await Table
                .FirstOrDefaultAsync(
                    x => x.UserID == userID &&
                         x.GroupID == groupID,
                    cancellationToken);

            if (entity == null)
                return false;

            await DeleteAsync(entity, cancellationToken);

            return true;
        }

    }
}
