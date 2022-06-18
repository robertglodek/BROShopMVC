using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.OrderBill
{
    public class AddOrderBillCommand:ICommand
    {
        [Display(Name ="Nazwa firmy")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        public string CompanyName { get; set; }

        [Display(Name = "NIP")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        public string NIP { get; set; }

        [Display(Name = "Ulica i numer domu")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        public string Street { get; set; }

        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        [RegularExpression("[0-9]{2}-[0-9]{3}", ErrorMessage = "Nieprawidłowy format: xx-xxx")]
        public string PostalCode { get; set; }
    }
}
