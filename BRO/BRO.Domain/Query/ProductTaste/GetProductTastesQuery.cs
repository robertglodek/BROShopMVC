using BRO.Domain.Query.DTO.ProductTaste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.ProductTaste
{
    public class GetProductTastesQuery:IQuery<List<ProductTasteDTO>>
    {
        public Guid ProductId { get; set; }
    }
}
