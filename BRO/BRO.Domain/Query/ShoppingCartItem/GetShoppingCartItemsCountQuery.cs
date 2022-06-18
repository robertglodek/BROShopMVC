using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.ShoppingCartItem
{
    public class GetShoppingCartItemsCountQuery:IQuery<int>
    { 
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
