
using BRO.Domain.Query.DTO.OrderHeader;
using BRO.Domain.Query.DTO.ProductTaste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DTO.OrderDetails
{
    public class OrderDetailsDTO
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        public OrderHeaderDTO Order { get; set; }


        public Guid ProductTasteId { get; set; }
        public ProductTasteDTO ProductTaste { get; set; }

        public int Count { get; set; }
        public double PricePerProduct { get; set; }

        public double WeightGramPerProduct { get; set; }
    }
}
