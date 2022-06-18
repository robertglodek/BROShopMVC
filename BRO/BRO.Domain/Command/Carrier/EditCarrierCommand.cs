using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Carrier
{
    public class EditCarrierCommand:ICommand
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(50, ErrorMessage = "Maksymalna liczba znaków: 50")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [Display(Name = "Czas dostawy")]
        [MaxLength(50, ErrorMessage = "Maksymalna liczba znaków: 50")]
        public string DeliveryTimeScope { get; set; }


        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [Range(10, 100000, ErrorMessage = "Niepoprawna liczba: przedział 10 - 100000")]
        [RegularExpression("[0-9]*[,]?[0-9]{1,2}", ErrorMessage = "Zawartość kolumny musi być liczbą")]
        [Display(Name = "Darmowa dostawa od zł")]
        public double FreeShippingFromPrice { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [Range(5, 100000, ErrorMessage = "Niepoprawna liczba: przedział 5 - 100000")]
        [RegularExpression("[0-9]*[,]?[0-9]{1,2}", ErrorMessage = "Zawartość kolumny musi być liczbą")]
        [Display(Name = "Koszt dostawy zł")]
        public double ShippingCost { get; set; }

        [Display(Name = "Dostępność")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Przedpłata")]
        public bool Prepayment { get; set; }

        public string Image { get; set; }
    }
}
