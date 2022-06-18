using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class LoginApplicationUserCommand:ICommand
    {

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]  
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }
}
