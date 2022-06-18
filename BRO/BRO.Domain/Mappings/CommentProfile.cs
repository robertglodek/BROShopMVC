using AutoMapper;
using BRO.Domain.Command.Comment;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Profiles
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Comment, AddCommentCommand>().ReverseMap();
            CreateMap<Comment, EditCommentCommand>().ReverseMap();
            CreateMap<CommentDTO, EditCommentCommand>().ReverseMap();
        }
    }
}
