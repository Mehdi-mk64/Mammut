
using Entities.Basic.SMS;



namespace DAL.Repository.Basic.SMS

{
    public class SendStatusRepository : Repository<SendStatus>
    {
        public SendStatusRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
