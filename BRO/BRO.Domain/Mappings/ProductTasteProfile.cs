using AutoMapper;
using BRO.Domain.Command.ProductTaste;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.ProductTaste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Profiles
{
    public class ProductTasteProfile:Profile
    {
        public ProductTasteProfile()
        {
            CreateMap<ProductTaste, ProductTasteDTO>().ReverseMap();
            CreateMap<ProductTaste, EditProductTasteCommand>().ReverseMap();
            CreateMap<EditProductTasteCommand, ProductTasteDTO>().ReverseMap();
        }
    }
}
