using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    /// <summary>
    /// Class used to remember user's password
    /// </summary>
    public class ForgotPasswordCommand:ICommand
    {
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu email")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
