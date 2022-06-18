using BRO.Domain.Query.DTO.ShoppingCartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.ShoppingCartItem
{

    public class GetShoppingCartItemsForItemsQuery : IQuery<List<ShoppingCartItemDTO>>
    {
        public List<ShoppingCartItemBasicDTO> ProductTasteIds { get; set; }
    }
}
