using AutoMapper;
using BRO.Domain.Command.ShoppingCart;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.ShoppingCartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Profiles
{
    public class ShoppingCartItemProfile:Profile
    {
        public ShoppingCartItemProfile()
        {
            CreateMap<ShoppingCartItem, ShoppingCartItemDTO>().ReverseMap();
            CreateMap<ShoppingCartItem, AddShoppingCartItemCommand>().ReverseMap();
        }
    }
}
