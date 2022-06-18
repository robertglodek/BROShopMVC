using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Entities
{
    public class OrderBill
    {
        public Guid Id { get; set; }
        [MaxLength(250)]
        public string CompanyName { get; set; }
        public string NIP { get; set; }
        [MaxLength(250)]
        public string Street { get; set; }
        [MaxLength(250)]
        public string City { get; set; }
        [MaxLength(250)]
        public string PostalCode { get; set; }
        public OrderHeader Order { get; set; }  
    }
}
