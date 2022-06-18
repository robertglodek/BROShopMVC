using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ShoppingCart
{
    public class AddShoppingCartItemCommand:ICommand
    {
        public Guid ApplicationUserId { get; set; }

        [Required(ErrorMessage = "Należy wybrać smak produktu")]
        public Guid ProductTasteId { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        public int Count { get; set; }
    }
}
