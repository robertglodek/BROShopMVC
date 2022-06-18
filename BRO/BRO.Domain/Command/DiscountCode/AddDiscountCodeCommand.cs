using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.DiscountCode
{
    public class AddDiscountCodeCommand : ICommand
    {
        [Display(Name = "Nazwa kodu")]
        [MinLength(3, ErrorMessage = "Minimalna liczba znaków: 3")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        public string CodeName { get; set; }


        
        [Display(Name = "Liczba użyć")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [Range(0, 100000, ErrorMessage = "Niepoprawna liczba: przedział 0 - 100000")]
        [RegularExpression("[0-9]*", ErrorMessage = "Zawartość kolumny musi być liczbą")]
        public int NumberOfUsesLeft { get; set; }


  
        [Display(Name = "Data zakończenia")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        public DateTimeOffset CodeAvailabilityEnd { get; set; }



        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [Range(1, 80, ErrorMessage = "Niepoprawna liczba: przedział 1 - 80")]
        [RegularExpression("[0-9]*", ErrorMessage = "Zawartość kolumny musi być liczbą")]
        [Display(Name = "Wartość promocji %")]
        public int DiscountInPercent { get; set; }


        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [RegularExpression("[0-9]*[,]?[0-9]{1,2}", ErrorMessage = "Zawartość kolumny musi być liczbą")]
        [Range(0, 100000, ErrorMessage = "Niepoprawna liczba: przedział 0 - 100000")]
        [Display(Name = "Minimalna cena produktów zł")]
        public double MinimumOrderPrice { get; set; }
 
    }
 
}
