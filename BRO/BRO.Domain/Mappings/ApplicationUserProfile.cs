using AutoMapper;
using BRO.Domain.Command.ApplicationUser;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Mappings
{
    public class ApplicationUserProfile:Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();
            CreateMap<LockoutApplicationuserCommand, ApplicationUserDTO>().ReverseMap();
            CreateMap<ApplicationUser, EditApplicationUserCommand>().ReverseMap();
            CreateMap<ApplicationUserDTO, EditApplicationUserCommand>().ReverseMap();
            CreateMap<ApplicationUserDTO, ChangeConfirmedEmailCommand>().ReverseMap();
        }
    }
}
