using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Entities
{
    public class DiscountCode
    {
        public Guid Id { get; set; }
        [MaxLength(250)]
        public string CodeName { get; set; }
        public int NumberOfUsesLeft { get; set; }
        public DateTimeOffset CodeAvailabilityEnd { get; set; }
        public int DiscountInPercent { get; set; }
        public double MinimumOrderPrice { get; set; }
        public List<OrderHeader> Orders { get; set; }
    }
}
