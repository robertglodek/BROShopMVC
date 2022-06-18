using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class ConfirmEmailCommand:ICommand
    {

        public string Email { get; set; }
        public string token { get; set; }
    }
}
