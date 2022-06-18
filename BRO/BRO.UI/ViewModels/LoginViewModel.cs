using BRO.Domain.Command.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.ViewModels
{
    public class LoginViewModel
    {
        public LoginApplicationUserCommand Command { get; set; }
        public string ReturnUrl { get; set; }
    }
}
