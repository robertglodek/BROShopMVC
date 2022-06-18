using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    /// <summary>
    /// Add new user Command class
    /// </summary>
    public class AddApplicationUserCommand:ICommand
    {
       
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [EmailAddress(ErrorMessage ="Nieprawidłowy format adresu email")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [RegularExpression("[0-9]{9}",ErrorMessage ="Nieprawidłowy format: xxxxxxxxx")]
        [Display(Name ="Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MinLength(8,ErrorMessage = "Minimalna liczba znaków: 8")]
        [MaxLength(50, ErrorMessage = "Maksymalna liczba znaków: 50")]
        public string Password { get; set; }

        [Display(Name = "Powtórz hasło")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Podane hasła różnią się")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MinLength(8, ErrorMessage = "Minimalna liczba znaków: 8")]
        [MaxLength(50, ErrorMessage = "Maksymalna liczba znaków: 50")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Rola")]
        public string Role { get; set; }

    }
}
