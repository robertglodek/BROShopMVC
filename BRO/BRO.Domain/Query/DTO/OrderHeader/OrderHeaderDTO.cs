using BRO.Domain.Query.DTO.ApplicationUser;
using BRO.Domain.Query.DTO.Carrier;
using BRO.Domain.Query.DTO.OrderDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DTO.OrderHeader
{
    public class OrderHeaderDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public ApplicationUserDTO User { get; set; }
        public DateTimeOffset? OrderDate { get; set; }
        public DateTimeOffset? ShippingDate { get; set; }
        public DateTimeOffset? DeliveryDate { get; set; }
        public double OrderProductsTotal { get; set; }
        public string TrackingNumber { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public DateTimeOffset? PaymentDate { get; set; }
        public Guid CarrierId { get; set; }
        public double OrderShippingCost { get; set; }
        public string TransactionId { get; set; }
        public int OrderNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PaymentMethod { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string ConcurrencyStamp { get; set; }

        [Display(Name = "Wiadomość do użytkownika")]
        public string MessageToUser { get; set; }
        public int DiscountInPercent { get; set; }

    }
}
