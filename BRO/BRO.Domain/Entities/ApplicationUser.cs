using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BRO.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
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
        public DateTimeOffset CreateDate { get; set; }
        [MaxLength(500)]
        public string LockoutReason { get; set; }
        public List<OrderHeader> Orders { get; set; }
        public string CustomerPaymentId { get; set; }
        public virtual List<UserRole> UserRoles { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Review> Reviews { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
