using Entities.Basic.Personel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repository.Base
{
    public interface IGroupRepository : IRepository<Entities.Basic.Personel.Group>
    {
        Task<List<string>> GetPhoneNumbersByGroupIDAsync(
            long groupID,
            CancellationToken cancellationToken);
    }
}
