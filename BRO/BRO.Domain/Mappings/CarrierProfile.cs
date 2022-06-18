using AutoMapper;
using BRO.Domain.Command.Carrier;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.Carrier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Mappings
{
    public class CarrierProfile:Profile
    {
        public CarrierProfile()
        {
            CreateMap<Carrier, CarrierDTO>().ReverseMap();
            CreateMap<AddCarrierCommand, Carrier>().ReverseMap();
            CreateMap<EditCarrierCommand, Carrier>().ReverseMap();
            CreateMap<EditCarrierCommand, CarrierDTO>().ReverseMap();
        }
    }
}
