using BRO.Domain.Query.DTO.OrderHeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Order
{
    public class GetOrderQuery:IQuery<OrderHeaderDTO>
    {
        public Guid Id { get; set; }
    }
}
