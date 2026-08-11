using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class PersonGroupSearchDto
    {
        public long PersonGroupID { get; set; }

        public long PersonID { get; set; }

        public string PersonCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }
    }
}
