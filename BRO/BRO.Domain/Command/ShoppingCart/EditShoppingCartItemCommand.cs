using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ShoppingCart
{
    public class EditShoppingCartItemCommand:ICommand
    {
        public Guid ShoppingCartItemId { get; set; }
        public int Count { get; set; }
    }  
}
