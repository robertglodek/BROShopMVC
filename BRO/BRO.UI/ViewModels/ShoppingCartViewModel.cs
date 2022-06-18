using BRO.Domain.Query.DTO.ShoppingCartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCartItemDTO> Cart { get; set; }
        public double OrderProductsTotal { get; set; }
    }
}
