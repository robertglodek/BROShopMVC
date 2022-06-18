using BRO.Domain.Command.OrderHeader;
using BRO.Domain.Query.DTO.DiscountCode;
using BRO.Domain.Query.DTO.OrderBill;
using BRO.Domain.Query.DTO.OrderDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.ViewModels
{
    public class OrderDetailsViewModel
    {
        [Display(Name = "Email użytkownika")]
        public string UserEmail { get; set; }
        [Display(Name = "Dostawca")]
        public string Carrier { get; set; }
        public EditOrderCommand Command { get; set; }
        public OrderBillDTO OrderBill { get; set; }
        public DiscountCodeDTO Code { get; set; }
        public List<OrderDetailsDTO> OrderDetails { get; set; }
    }
}
