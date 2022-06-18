using AutoMapper;
using BRO.Domain.Command.Taste;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.Taste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Profiles
{
    public class TasteProfile:Profile
    {
        public TasteProfile()
        {
            CreateMap<Taste, TasteDTO>().ReverseMap();
            CreateMap<Taste, AddTasteCommand>().ReverseMap();
            CreateMap<Taste, EditTasteCommand>().ReverseMap();
            CreateMap<EditTasteCommand, TasteDTO>().ReverseMap();
            CreateMap<DeleteTasteCommand, TasteDTO>().ReverseMap();
        }
    }
}
