using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Entities
{
    public class OrderDetails
    {
        public Guid Id { get; set; }
        public Guid OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public Guid ProductTasteId { get; set; }
        public ProductTaste ProductTaste { get; set; }
        public int Count { get; set; }
        public double PricePerProduct { get; set; }    
    }
}
