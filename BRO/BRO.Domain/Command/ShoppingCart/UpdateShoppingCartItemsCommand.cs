using BRO.Domain.Query.DTO.ShoppingCartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ShoppingCart
{
    public class UpdateShoppingCartItemsCommand : ICommand
    {
        public Guid UserId { get; set; }
        public List<ShoppingCartItemBasicDTO> Items { get ; set; }
    }
}
