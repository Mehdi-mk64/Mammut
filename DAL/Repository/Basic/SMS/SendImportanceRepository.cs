
using Entities.Basic.SMS;



namespace DAL.Repository.Basic.SMS

{
    public class SendImportanceRepository : Repository<SendImportance>
    {
        public SendImportanceRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
