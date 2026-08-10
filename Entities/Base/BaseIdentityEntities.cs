using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Base
{

    public abstract class BaseIdentityEntities<TKey> : IdentityUser<int> ,IEntity
    {
        public TKey ID { get; set; }
    }
}
