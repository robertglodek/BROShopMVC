using AutoMapper;
using BRO.Domain.Command.DiscountCode;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.DiscountCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Profiles
{
    public class DiscountCodeProfile:Profile
    {
        public DiscountCodeProfile()
        {
            CreateMap<DiscountCode, DiscountCodeDTO>().ReverseMap();
            CreateMap<DiscountCode, AddDiscountCodeCommand>().ReverseMap();
            CreateMap<DiscountCode, EditDiscountCodeCommand>().ReverseMap();
            CreateMap<DiscountCodeDTO, AddDiscountCodeCommand>().ReverseMap();
            CreateMap<DiscountCodeDTO, EditDiscountCodeCommand>().ReverseMap();
        }
    }
}
