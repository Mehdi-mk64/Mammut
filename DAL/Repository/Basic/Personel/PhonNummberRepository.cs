
using Entities.Basic.Personel;
using Entities.Basic.SMS;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repository.Basic.Personal
{
    public class PhonNummberRepository : Repository<PhonNumbers>
    {
        public PhonNummberRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public PhonNumbers GetByNumber(string phonNummber)
        {
            return Table.Where(w => w.Nummber == phonNummber).FirstOrDefault<PhonNumbers>();
        }

    }
}
