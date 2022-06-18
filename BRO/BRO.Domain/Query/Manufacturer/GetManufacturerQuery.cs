using BRO.Domain.Query.DTO.Manufacturer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Manufacturer
{
    public class GetManufacturerQuery:IQuery<ManufacturerDTO>
    {
        public Guid Id { get; set; }
    }
}
