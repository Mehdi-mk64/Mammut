
using Entities.Basic.SMS;
using System.Linq;

namespace DAL.Repository.Basic.SMS

{
    public class SendProviderRepository : Repository<Entities.Basic.SMS.SMSProvider>
    {
        public SendProviderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public SMSProvider GetByName(string smsProviderTitle)
        {
            return Table.Where(w=>w.Title==smsProviderTitle).FirstOrDefault<SMSProvider>();
        }
    }
}
