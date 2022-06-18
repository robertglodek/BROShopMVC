using BRO.Domain.Query.DTO.Product;
using BRO.Domain.Query.DTO.Taste;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DTO.ProductTaste
{
    public class ProductTasteDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public Guid TasteId { get; set; }
        public TasteDTO Taste { get; set; }
        public int InStock { get; set; }
    }
}
