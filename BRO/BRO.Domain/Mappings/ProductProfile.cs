using AutoMapper;
using BRO.Domain.Command.Product;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDetailsDTO, AddProductCommand>().ReverseMap();
            CreateMap<Product, AddProductCommand>().ReverseMap();
            CreateMap<ProductDetailsDTO, EditProductCommand>().ReverseMap();
            CreateMap<Product, EditProductCommand>().ReverseMap();
            CreateMap<Product, ProductDetailsDTO>().ForMember(n => n.Rating, y => y.MapFrom(n => n.Reviews.Count() > 0 ? (n.Reviews.Sum(s => s.Rating) / n.Reviews.Count()) : 0))
                .ForMember(n => n.ReviewsNumber, y => y.MapFrom(n => n.Reviews.Count())).ReverseMap();
            CreateMap<Product, ProductDTO>().ForMember(n => n.Rating, y => y.MapFrom(n => n.Reviews.Count() > 0 ? (n.Reviews.Sum(s => s.Rating) / n.Reviews.Count()) : 0))
                .ForMember(n => n.ReviewsNumber, y => y.MapFrom(n => n.Reviews.Count())).ReverseMap();
        }
    }
}
