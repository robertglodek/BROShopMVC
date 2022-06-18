using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Entities
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public override Guid RoleId { get; set; }
        public Role Role { get; set; }
        public override Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
