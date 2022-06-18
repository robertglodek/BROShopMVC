using AutoMapper;
using BRO.Domain;
using BRO.Domain.IRepositories;
using BRO.Domain.Mappings;
using BRO.Domain.Query.Review;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BRO.Test.Unit.QueryTests.Review
{
    public class GetReviewQueryTest
    {
        [Fact]
        public async Task GetReview_WhenExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var query = new GetReviewQuery() { ProductId=Guid.NewGuid(), UserId=Guid.NewGuid()  };
                var review = sut.CreateReview("Super produkt");
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.ReviewRepository.GetByProductIdAndUserId(query.UserId,query.ProductId).ReturnsForAnyArgs(Task.FromResult<BRO.Domain.Entities.Review>(review));
               
                var queryHandler = new GetReviewQueryHandler(unitOfWorkSubstitute,mapper);
                var result = await queryHandler.HandleAsync(query);
                result.Content.Should().Be("Super produkt");
            }
        }
    }
}
