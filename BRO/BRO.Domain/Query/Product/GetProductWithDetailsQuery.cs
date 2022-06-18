using BRO.Domain.Query.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Product
{
    
    public class GetProductWithDetailsQuery : IQuery<ProductDetailsDTO>
    {
        public Guid Id { get; set; }
    }
}
