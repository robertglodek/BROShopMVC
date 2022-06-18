using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Entities
{
   
    public class ShoppingCartItem
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid ProductTasteId { get; set; }
        public ProductTaste ProductTaste { get; set; }
        public int Count { get; set; }
    }
}
