using BRO.Domain.Query.DTO.ApplicationUser;
using BRO.Domain.Query.DTO.Carrier;
using BRO.Domain.Query.DTO.DiscountCode;
using BRO.Domain.Query.DTO.OrderBill;
using BRO.Domain.Query.DTO.OrderDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DTO.OrderHeader
{
    public class OrderHeaderDetailsDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUserDTO User { get; set; }


        [Display(Name = "Data złożenia zamówienia")]
        public DateTimeOffset? OrderDate { get; set; }


        [Display(Name = "Data wysyłki")]
        public DateTimeOffset? ShippingDate { get; set; }


        [Display(Name = "Data dostawy")]
        public DateTimeOffset? DeliveryDate { get; set; }
        public virtual List<OrderDetailsDTO> OrderDetails { get; set; }
        public double OrderProductsTotal { get; set; }


        [Display(Name = "Nr. sledzenia przesyłki")]
        public string TrackingNumber { get; set; }

        [Display(Name = "Status zamówienia")]
        public string OrderStatus { get; set; }

        [Display(Name = "Status płatności")]
        public string PaymentStatus { get; set; }

        [Display(Name = "Data zapłaty")]
        public DateTimeOffset? PaymentDate { get; set; }
        public Guid CarrierId { get; set; }
        public CarrierDTO Carrier { get; set; }
        public Guid? OrderBillId { get; set; }
        public OrderBillDTO OrderBill { get; set; }
        public double OrderShippingCost { get; set; }
        public string TransactionId { get; set; }
        public int OrderNumber { get; set; }

        [Display(Name = "Imie")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Ulica i nr. domu")]
        public string Street { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Metoda płatności")]
        public string PaymentMethod { get; set; }


        [Display(Name ="Kod pocztowy")]
        public string PostalCode { get; set; }

        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Wiadomość do sprzedawcy")]
        public string UserDescription { get; set; }

        [Display(Name = "Wiadomość od sprzedawcy")]
        public string EmployeeDescription { get; set; }
        public string ConcurrencyStamp { get; set; }
        public int DiscountInPercent { get; set; }
        public Guid? DiscountCodeId { get; set; }
        public DiscountCodeDTO DiscountCode { get; set; }

        [Display(Name = "Wiadomość do użytkownika")]
        public string MessageToUser { get; set; }

    }
}
