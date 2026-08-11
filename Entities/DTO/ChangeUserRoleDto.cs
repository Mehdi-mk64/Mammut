using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class ChangeUserRoleDto
    {
        public int UserId { get; set; }

        public string RoleName { get; set; }
    }
}
