using BRO.Domain.Query.DTO.Product;
using BRO.Domain.Query.DTO.Taste;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ProductTaste
{
    public class EditProductTasteCommand:ICommand
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public ProductDTO Product { get; set; }

        public Guid TasteId { get; set; }
        public TasteDTO Taste { get; set; }

        [Display(Name = "Ilość")]
        [Range(0, 100000, ErrorMessage = "Niepoprawna liczba: przedział 0 - 100000")]
        [RegularExpression("[0-9]*", ErrorMessage = "Zawartość kolumny musi być liczbą")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        public int InStock { get; set; }
    }
}
