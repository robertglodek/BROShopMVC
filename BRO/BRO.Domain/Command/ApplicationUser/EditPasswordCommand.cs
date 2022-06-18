using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{

    /// <summary>
    /// Class responsible for changing user password
    /// </summary>
    public class EditPasswordCommand:ICommand
    {

        public Guid Id { get; set; }



        [Display(Name = "Nowe hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MinLength(8, ErrorMessage = "Minimalna liczba znaków: 8")]
        [MaxLength(50, ErrorMessage = "Maksymalna liczba znaków: 50")]
        public string Password { get; set; }

        [Display(Name = "Powtórz nowe hasło")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Podane hasła różnią się")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MinLength(8, ErrorMessage = "Minimalna liczba znaków: 8")]
        [MaxLength(50, ErrorMessage = "Maksymalna liczba znaków: 50")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Obecne hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MinLength(8, ErrorMessage = "Minimalna liczba znaków: 8")]
        [MaxLength(50, ErrorMessage = "Maksymalna liczba znaków: 50")]  
        public string PasswordToConfirm { get; set; }
    }
}
