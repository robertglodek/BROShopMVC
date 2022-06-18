using AutoMapper;
using BRO.Domain.Command.OrderHeader;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.OrderHeader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<AddOrderCommand, OrderHeader>().ReverseMap();
            CreateMap<OrderHeader, OrderHeaderDTO>().ReverseMap();
            CreateMap<OrderHeader, OrderHeaderDetailsDTO>().ReverseMap();
            CreateMap<EditOrderCommand, OrderHeaderDetailsDTO>().ReverseMap();
            CreateMap<EditOrderCommand, OrderHeaderDTO>().ReverseMap();
            CreateMap<EditOrderCommand, OrderHeader>().ReverseMap();
        }
    }
}
