using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class ResetPasswordCommand:ICommand
    {
        public string Email { get; set; }
        public string Token { get; set; }

        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MinLength(8, ErrorMessage = "Minimalna liczba znaków: 8")]
        [MaxLength(50, ErrorMessage = "Maksymalna liczba znaków: 50")]
        public string Password { get; set; }

        [Display(Name = "Powtórz hasło")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Podane hasła różnią się")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MinLength(8, ErrorMessage = "Minimalna liczba znaków: 8")]
        [MaxLength(50, ErrorMessage = "Maksymalna liczba znaków: 50")]
        public string ConfirmPassword { get; set; }
    }
}
