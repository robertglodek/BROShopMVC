using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DTO.ShoppingCartItem
{
    public class ShoppingCartItemBasicDTO
    {
        public Guid Id { get; set; }
        public Guid ProductTasteId { get; set; }
        public int Count { get; set; }
    }
}
