using BRO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Test.Unit.Models
{
    public class TasteProxy:Taste
    {
        public TasteProxy(string name)
        {
            this.Name = name;
            this.ProductTastes = new List<ProductTaste>();
        }
    }
}
