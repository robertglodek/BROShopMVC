using BRO.Domain.Query.DTO.Carrier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.ViewModels
{
    public class OrderCarrierViewModel
    {
        public Guid CarrierId { get; set; }
        public double OrderProductsTotal { get; set; }
        public double OrderShippingCost { get; set; }
        public string DiscountCode { get; set; }
        public List<CarrierDTO> Carriers { get; set; }
    }
}
