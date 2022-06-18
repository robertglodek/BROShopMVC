using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DTO.ApplicationUser
{
    public class ApplicationUserDTO
    {
        public Guid Id { get; set; }
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Ulica i numer domu")]
        public string Street { get; set; }
        [Display(Name = "Miasto")]
        public string City { get; set; }
        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }
        public bool EmailConfirmed { get; set; }
        public string LockoutReason { get; set; }
        public virtual DateTimeOffset? LockoutEnd { get; set; }

        public string RoleName { get; set; }
    }
}
