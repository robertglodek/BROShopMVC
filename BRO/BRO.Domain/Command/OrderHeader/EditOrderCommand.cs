using BRO.Domain.Command.OrderBill;
using BRO.Domain.Query.DTO.Carrier;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.OrderHeader
{
    public class EditOrderCommand:ICommand
    {
        public Guid Id { get; set; }

        [Display(Name = "Status zamówienia")]
        public string OrderStatus { get; set; }

        [Display(Name = "Status płatności")]
        public string PaymentStatus { get; set; }
        public Guid CarrierId { get; set; }

        [Display(Name = "Id transakcji")]
        public string TransactionId { get; set; }

        [Display(Name = "Metoda płatności")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Ulica i numer domu")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        public string Street { get; set; }

        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [RegularExpression("[0-9]{2}-[0-9]{3}", ErrorMessage = "Nieprawidłowy format: xx-xxx")]
        public string PostalCode { get; set; }

        [RegularExpression("[0-9]{9}", ErrorMessage = "Nieprawidłowy format: xxxxxxxxx")]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        public string LastName { get; set; }

        public bool IsOrderBill { get; set; }

        [Display(Name = "Wiadomość od użytkownika")]
        [MaxLength(200, ErrorMessage = "Maksymalna liczba znaków: 200")]
        public string UserDescription { get; set; }

        [Display(Name = "Koszt dostawy")]
        public double OrderShippingCost { get; set; }

        [Display(Name = "Koszt produktów")]
        public double OrderProductsTotal { get; set; }

        [Display(Name = "Numer śledzenia przesyłki")]
        [MaxLength(100, ErrorMessage = "Maksymalna liczba znaków: 100")]
        public string TrackingNumber { get; set; }

        [Display(Name = "Data wysłania")]
        public DateTimeOffset? ShippingDate { get; set; }

        [Display(Name = "Data płatności")]
        public DateTimeOffset? PaymentDate { get; set; }

        [Display(Name = "Data zamówienia")]
        public DateTimeOffset? OrderDate { get; set; }

        [Display(Name = "Data dostawy")]
        public DateTimeOffset? DeliveryDate { get; set; }

        public string ConcurrencyStamp { get; set; }

        [Display(Name = "Wiadomość do użytkownika")]
        [MaxLength(200, ErrorMessage = "Maksymalna liczba znaków: 200")]
        public string MessageToUser { get; set; }

        public int DiscountInPercent { get; set; }
    }
}
