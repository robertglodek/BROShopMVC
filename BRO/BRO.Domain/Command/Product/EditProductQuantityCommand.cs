using BRO.Domain.Query.DTO.ProductTaste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ProductTaste
{
    public class EditProductQuantityCommand:ICommand
    {
        public Guid ProductId { get; set; }
        public string ProductConcurrencyStamp { get; set; }
        public List<EditProductTasteCommand> ProductTastes { get; set; }
    }
}
