
using Entities.Basic.Personel;


namespace DAL.Repository.Basic.Personal

{
    public class GroupRepository : Repository<Group>
    {
        public GroupRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

    }
}
