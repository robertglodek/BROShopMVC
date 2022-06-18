using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Entities
{
    public class Carrier
    { 
        public Guid Id { get; set; }
        public bool IsAvailable { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string DeliveryTimeScope { get; set; }
        public double FreeShippingFromPrice { get; set; }
        public double ShippingCost { get; set; }
        public bool Prepayment { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        public List<OrderHeader> Orders { get; set; }
    }
}
