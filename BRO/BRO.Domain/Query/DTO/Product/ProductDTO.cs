using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.Category;
using BRO.Domain.Query.DTO.Manufacturer;

namespace BRO.Domain.Query.DTO.Product
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float WeightGram { get; set; }
        public int Portions { get; set; }
        public double RegularPrice { get; set; }
        public double PromotionalPrice { get; set; }
        public bool IsDiscount { get; set; }
        public string SearchTag { get; set; }
        public bool Availability { get; set; }
        public string ImageUrlLarge { get; set; }
        public string ImageUrlMain { get; set; }
        public DateTimeOffset ProductAddDate { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
        public Guid ManufacturerId { get; set; }
        public ManufacturerDTO Manufacturer { get; set; }
        public double Rating { get; set; }
        public int ReviewsNumber { get; set; }
        public string ConcurrencyStamp { get; set; }

    }
}
