using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Taste
{
    public class EditTasteCommand:ICommand
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
    }
}
