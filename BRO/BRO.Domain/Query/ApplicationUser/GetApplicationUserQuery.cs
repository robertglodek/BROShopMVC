using BRO.Domain.Query.DTO.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.ApplicationUser
{
    public class GetApplicationUserQuery:IQuery<ApplicationUserDTO>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
