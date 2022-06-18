using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Entities
{
    public class OrderHeader
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTimeOffset? OrderDate { get; set; }
        public DateTimeOffset? ShippingDate { get; set; }
        public DateTimeOffset? PaymentDate { get; set; }
        public DateTimeOffset? DeliveryDate { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public double OrderProductsTotal { get; set; }
        [MaxLength(250)]
        public string TrackingNumber { get; set; }
        [MaxLength(250)]
        public string OrderStatus { get; set; }
        [MaxLength(250)]
        public string PaymentStatus { get; set; }
        public Guid CarrierId { get; set; }
        public Carrier Carrier { get; set; }
        public double OrderShippingCost { get; set; }
        public string TransactionId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderNumber { get; set; }
        [MaxLength(250)]
        public string PaymentMethod { get; set; }
        [MaxLength(250)]
        public string FirstName { get; set; }
        [MaxLength(250)]
        public string LastName { get; set; }
        [MaxLength(250)]
        public string Street { get; set; }
        [MaxLength(250)]
        public string City { get; set; }
        [MaxLength(250)]
        public string PostalCode { get; set; }
        [MaxLength(250)]
        public string PhoneNumber { get; set; }
        [MaxLength(250)]
        public string UserDescription { get; set; }
        public Guid? OrderBillId { get; set; }
        public OrderBill OrderBill { get; set; }
        [MaxLength(250)]
        public string ConcurrencyStamp { get; set; }
        public int DiscountInPercent { get; set; }
        public Guid? DiscountCodeId { get; set; }
        public DiscountCode DiscountCode { get; set; }
        [MaxLength(500)]
        public string MessageToUser { get; set; }
    }
}
