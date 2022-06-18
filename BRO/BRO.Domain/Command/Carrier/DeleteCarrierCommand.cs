using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Carrier
{
    public class DeleteCarrierCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
