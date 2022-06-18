using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.Comment;
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

namespace BRO.Test.Unit.CommandTests.Comment
{
    public class AddCommentCommandTest
    {

        [Fact]
        public async Task AddComment_WhenNameLengthTooBig_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddCommentCommand
                {
                    Content= "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                    "when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, " +
                    "remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software " +
                    "like Aldus PageMaker including versions of Lorem Ipsum.Why do we use it ?It is a long established fact that a reader will be distracted by the readable content of a page when looking at " +
                    "its layout. The point of using Lorem Ipsum is that it has a more - or - less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable" +
                    " English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still " +
                    "in their infancy.Various versions have evolved over the years, sometimes by accident, sometimes on purpose(injected humour and the like)"
                    
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                var handler = new AddCommentCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }

        [Fact]
        public async Task AddComment_WhenNameLengthTooSmall_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddCommentCommand
                {
                    Content = "L",
                    ProductId=Guid.NewGuid(),
                    UserId=Guid.NewGuid()
                    

                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();

                var handler = new AddCommentCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }

        [Fact]
        public async Task AddComment_WhenUserNotExists_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddCommentCommand
                {
                    Content = "Super produkt, polecam!!!",
                    ProductId = Guid.NewGuid(),
                    UserId = Guid.NewGuid()


                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();

                unitOfWorkSubstitute.ApplicationUserRepository.GetById(command.UserId).Returns(Task.FromResult<BRO.Domain.Entities.ApplicationUser>(null));
                unitOfWorkSubstitute.ProductRepository.GetById(command.ProductId).Returns(Task.FromResult<BRO.Domain.Entities.Product>(sut.CreateProduct("Batonik strik")));
                var handler = new AddCommentCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
    }
}
