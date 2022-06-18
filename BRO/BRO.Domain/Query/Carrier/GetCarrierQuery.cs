using BRO.Domain.Query.DTO.Carrier;
using BRO.Domain.Query.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Carrier
{
    public class GetCarrierQuery:IQuery<CarrierDTO>
    {
        public Guid Id { get; set; }
    }
}
