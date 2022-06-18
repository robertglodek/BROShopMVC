using BRO.Domain.Command.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.ViewModels
{
    public class ChangeEmailViewModel
    {
        public ChangeEmailCommand Command { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }
}
