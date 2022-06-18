using BRO.Domain.Query.DTO.OrderHeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Order
{
    public class GetOrdersQuery:IQuery<List<OrderHeaderDTO>>
    {
        public Guid UserId { get; set; }
    }
}
