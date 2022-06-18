using AutoMapper;
using BRO.Domain.Command.Review;
using BRO.Domain.Entities;
using BRO.Domain.Query.DTO.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Profiles
{
    public class ReviewProfile:Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Review, AddReviewCommand>().ReverseMap();
        }
    }
}
