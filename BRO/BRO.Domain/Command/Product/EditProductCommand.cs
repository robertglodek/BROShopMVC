using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Product
{
    public class EditProductCommand : ICommand
    {
        public Guid Id { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(50, ErrorMessage = "Maksymalna liczba znaków: 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [Range(1, 1000, ErrorMessage = "Niepoprawna liczba: przedział 10 - 1000")]
        [RegularExpression("[0-9]*", ErrorMessage = "Zawartość kolumny musi być liczbą")]
        [Display(Name = "Porcje")]
        public int Portions { get; set; }

        
        [Display(Name = "Użycie")]
        [MaxLength(300, ErrorMessage = "Maksymalna liczba znaków: 300")]
        public string Use { get; set; }

        [Display(Name = "Opis")]
        [MaxLength(300, ErrorMessage = "Maksymalna liczba znaków: 300")]
        public string Description { get; set; }

        [Display(Name = "Informacje żywieniowe")]
        [MaxLength(300, ErrorMessage = "Maksymalna liczba znaków: 300")]
        public string NutritionalInformation { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [Range(1, 100000, ErrorMessage = "Niepoprawna liczba: przedział 1 - 100000")]
        [RegularExpression("[0-9]*", ErrorMessage = "Zawartość kolumny musi być liczbą")]
        [Display(Name = "Waga g")]
        public float WeightGram { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [Range(2, 100000, ErrorMessage = "Niepoprawna liczba: przedział 2 - 100000")]
        [RegularExpression("[0-9]*[,]?[0-9]{1,2}", ErrorMessage = "Zawartość kolumny musi być liczbą")]
        [Display(Name = "Cena zł")]
        public double RegularPrice { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [Range(2, 100000, ErrorMessage = "Niepoprawna liczba: przedział 2 - 100000")]
        [RegularExpression("[0-9]*[,]?[0-9]{1,2}", ErrorMessage = "Zawartość kolumny musi być liczbą")]
        [Display(Name = "Cena promocyjna zł")]
        public double PromotionalPrice { get; set; }

        [Display(Name = "Promocja")]
        public bool IsDiscount { get; set; }

        [Display(Name = "Dostępność")]
        public bool Availability { get; set; }

        [Display(Name = "Fraza do wyszukiwania")]
        public string SearchTag { get; set; }
        public string ImageUrlLarge { get; set; }
        public string ImageUrlMain { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        public Guid ManufacturerId { get; set; }

        [MinLength(1, ErrorMessage = "Produkt musi posiadać przynajmniej jeden smak")]
        public IEnumerable<Guid> TasteIds { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}
