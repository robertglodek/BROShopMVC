using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class SignInCommand:ICommand
    { 
        public string Email { get; set; }
        public bool RememberMe { get; set; }
    }
}
