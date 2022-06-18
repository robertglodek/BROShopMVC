using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class ChangeConfirmedEmailCommand:ICommand
    {
        public Guid Id { get; set; }
        public string NewEmail { get; set; }
        public string Token { get; set; }
    }
}
