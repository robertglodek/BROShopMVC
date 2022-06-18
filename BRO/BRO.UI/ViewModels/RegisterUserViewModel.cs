using BRO.Domain.Command.ApplicationUser;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.ViewModels
{
    public class RegisterUserViewModel
    {
        public AddApplicationUserCommand Command { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
