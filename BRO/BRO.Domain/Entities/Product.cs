using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        public int Portions { get; set; }
        [MaxLength(250)]
        public string Use { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [MaxLength(250)]
        public string NutritionalInformation { get; set; }
        public float WeightGram { get; set; }
        public bool Availability { get; set; }
        [MaxLength(250)]
        public string SearchTag { get; set; }
        public double RegularPrice { get; set; }
        public double PromotionalPrice { get; set; }
        public bool IsDiscount { get; set; }
        [MaxLength(250)]
        public string ImageUrlLarge { get; set; }
        [MaxLength(250)]
        public string ImageUrlMain { get; set; }
        public DateTimeOffset ProductAddDate { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public List<ProductTaste> ProductTastes { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Comment> Comments { get; set; }
        [MaxLength(250)]
        public string ConcurrencyStamp { get; set; }
    }
}
