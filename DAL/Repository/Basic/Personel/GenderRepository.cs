
using Entities.Basic.Personel;


namespace DAL.Repository.Basic.Personal

{
    
    public class GenderRepository : Repository<Gender>
    {
        public GenderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

  
    }
}
