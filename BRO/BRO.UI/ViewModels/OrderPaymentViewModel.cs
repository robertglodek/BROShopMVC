using BRO.Domain.Command.OrderHeader;
using BRO.Domain.Query.DTO.Carrier;
using BRO.Domain.Query.DTO.DiscountCode;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.ViewModels
{
    public class OrderPaymentViewModel
    {
        public AddOrderCommand Command { get; set; }
        public CarrierDTO Carrier { get; set; }
        public DiscountCodeDTO DiscountCode { get; set; }

    }
}
