using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.Comment;
using BRO.Domain.Command.Review;
using BRO.Domain.IRepositories;
using BRO.Domain.Mappings;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BRO.Test.Unit.CommandTests.Review
{
    public class AddReviewCommandTest
    {
        [Fact]
        public async Task AddReview_WhenValidationRatingTooHight_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddReviewCommand
                {
                    Rating=8
                    
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                var handler = new AddReviewCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
        [Fact]
        public async Task AddReview_WhenValidationRatingTooSmall_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddReviewCommand
                {
                    Rating = 0

                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                var handler = new AddReviewCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
        [Fact]
        public async Task AddReview_WhenValidationSuccess_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddReviewCommand
                {
                    Rating=3
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);
                }).CreateMapper();
                var handler = new AddReviewCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().Be(true);
            }
        }
    }
}
