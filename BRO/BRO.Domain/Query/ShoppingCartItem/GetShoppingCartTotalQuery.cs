using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.ShoppingCartItem
{
    public class GetShoppingCartTotalQuery:IQuery<double>
    {
        public Guid UserId { get; set; }
    }
}
