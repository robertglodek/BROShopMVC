using AutoMapper;
using BRO.Domain.Command.OrderBill;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.OrderBill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Profiles
{
    public class OrderBillProfile:Profile
    {
        public OrderBillProfile()
        {
            CreateMap<AddOrderBillCommand, OrderBill>().ReverseMap();
            CreateMap<OrderBillDTO, OrderBill>().ReverseMap();
        }
    }
}
