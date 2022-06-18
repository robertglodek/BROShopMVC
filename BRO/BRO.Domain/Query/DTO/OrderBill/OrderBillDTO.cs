using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DTO.OrderBill
{
    public class OrderBillDTO
    {
        public Guid Id { get; set; }


        [Display(Name = "Nazwa firmy")]
        public string CompanyName { get; set; }

        [Display(Name = "NIP")]
        public string NIP { get; set; }


        [Display(Name = "Ulica i nr. domu")]
        public string Street { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }


        [Display(Name ="Kod pocztowy")]
        public string PostalCode { get; set; }
       
    }
}
