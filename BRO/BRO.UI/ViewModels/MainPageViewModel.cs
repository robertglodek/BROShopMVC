using BRO.Domain.Query.DTO.Product;
using BRO.Domain.Query.DTO.ProductTaste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRO.UI.ViewModels
{
    public class MainPageViewModel
    {
        public List<ProductTasteDTO> Bestsellers { get; set; }
        public List<ProductDTO> Latest { get; set; }
    }
}
