using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.Product
{
    public class DeleteProductCommand : ICommand 
    { 
        public Guid Id { get; set; }
    }
}
