using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.DiscountCode
{
    public class DeleteDiscountCodeCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
