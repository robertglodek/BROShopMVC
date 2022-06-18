using AutoMapper;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.OrderDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Profiles
{
    public class OrderDetailsProfile:Profile
    {
        public OrderDetailsProfile()
        {
            CreateMap<OrderDetails, OrderDetailsDTO>().ReverseMap();
        }
    }
}
