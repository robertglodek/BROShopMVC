using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DTO.DiscountCode
{
    public class DiscountCodeDTO
    {
        public Guid Id { get; set; }

        public string CodeName { get; set; }

        public int NumberOfUsesLeft { get; set; }

        public DateTimeOffset CodeAvailabilityEnd { get; set; }

        public int DiscountInPercent { get; set; }

        public double MinimumOrderPrice { get; set; }
    }
}
