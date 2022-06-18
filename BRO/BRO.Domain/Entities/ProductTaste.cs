using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Entities
{
    public class ProductTaste
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid TasteId { get; set; }
        public Taste Taste { get; set; }
        public int InStock { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
