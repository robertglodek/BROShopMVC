using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DTO.Carrier
{
    public class CarrierDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DeliveryTimeScope { get; set; }

        public double FreeShippingFromPrice { get; set; }
  
        public bool IsAvailable { get; set; }

        public double ShippingCost { get; set; }

        public bool Prepayment { get; set; }

        public string Image { get; set; }


        public double ActualShippingCost { get; set; }

    }
}
