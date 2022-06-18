using AutoMapper;
using BRO.Domain.Command.Manufacturer;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.Manufacturer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Mappings
{
    public class ManufacturerProfile:Profile
    {
        public ManufacturerProfile()
        {
            CreateMap<Manufacturer, ManufacturerDTO>().ReverseMap();
            CreateMap<Manufacturer, AddManufacturerCommand>().ReverseMap();
            CreateMap<Manufacturer, EditManufacturerCommand>().ReverseMap();
            CreateMap<EditManufacturerCommand, ManufacturerDTO>().ReverseMap();
            CreateMap<DeleteManufacturerCommand, ManufacturerDTO>().ReverseMap();
        }
    }
}
