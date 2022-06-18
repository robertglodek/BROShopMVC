using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.ProductTaste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DTO.ShoppingCartItem
{
    public class ShoppingCartItemDTO
    {
        public Guid Id { get; set; }
        public Guid ProductTasteId { get; set; }
        public ProductTasteDTO ProductTaste { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
