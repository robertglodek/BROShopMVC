using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class EditApplicationUserCommand:ICommand
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [RegularExpression("[0-9]{9}", ErrorMessage = "Nieprawidłowy format: xxxxxxxxx")]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }


        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        [Display(Name = "Ulica i numer domu")]
        public string Street { get; set; }

        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

       
        [Display(Name = "Kod Pocztowy")]
        [RegularExpression("[0-9]{2}-[0-9]{3}", ErrorMessage = "Nieprawidłowy format: xx-xxx")]
        public string PostalCode { get; set; }
    }
}
