using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Entities
{
    public class Taste
    { 
        public Guid Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        public List<ProductTaste> ProductTastes { get; set; }
    }
}
