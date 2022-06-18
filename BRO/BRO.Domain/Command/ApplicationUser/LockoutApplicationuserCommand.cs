using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ApplicationUser
{
    public class LockoutApplicationuserCommand:ICommand
    {
        public Guid Id { get; set; }

        [Display(Name = "Powód blokady")]
        [MaxLength(100, ErrorMessage = "Maksymalna liczba znaków: 100")]
        public string LockoutReason { get; set; }

        [Display(Name = "Czas zakończenia blokady")]
        public virtual DateTimeOffset? LockoutEnd { get; set; }
    }
}
