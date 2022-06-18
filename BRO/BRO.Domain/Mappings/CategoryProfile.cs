using AutoMapper;
using BRO.Domain.Command.Category;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<AddCategoryCommand, Category>().ReverseMap();
            CreateMap<EditCategoryCommand, Category>().ReverseMap();
            CreateMap<EditCategoryCommand, CategoryDTO>().ReverseMap();
            CreateMap<DeleteCategoryCommand, CategoryDTO>().ReverseMap();
        }
    }
}
