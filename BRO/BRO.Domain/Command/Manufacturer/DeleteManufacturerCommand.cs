using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Manufacturer
{
    public class DeleteManufacturerCommand:ICommand
    {
        public Guid Id { get; set; }
    }
}
