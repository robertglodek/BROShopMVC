using BRO.Domain.Command.OrderBill;
using BRO.Domain.Query.DTO.Carrier;
using BRO.Domain.Query.DTO.OrderDetails;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.OrderHeader
{
    public class AddOrderCommand:ICommand
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CarrierId { get; set; }

        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [MaxLength(30, ErrorMessage = "Maksymalna liczba znaków: 30")]
        public string LastName { get; set; }

        [Display(Name = "Ulica i nr domu")]
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
        [Required(ErrorMessage = "Zawartość kolumny nie może być pusta")]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }
        public string PaymentMethod { get; set; }

        [Display(Name = "Wiadomość ")]
        public string UserDescription { get; set; }
        public bool IsOrderBill { get; set; }
        public AddOrderBillCommand OrderBillCommand { get; set; }
        public  Guid? DiscountCodeId { get; set; }
        public double OrderShippingCost { get; set; }
        public double OrderProductsTotal { get; set; }
        public double? OrderProductsTotalAfterDiscount { get; set; }
    }
}
