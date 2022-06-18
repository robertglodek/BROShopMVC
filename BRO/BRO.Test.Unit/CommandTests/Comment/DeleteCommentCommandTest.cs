using BRO.Domain.Command.Comment;
using BRO.Domain.IRepositories;
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
    public class DeleteCommentCommandTest
    {
        [Fact]
        public async Task DeleteComment_WhenNotExist_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteCommentCommand
                {
                    Id = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.CommentRepository.GetById(command.Id).Returns(Task.FromResult<BRO.Domain.Entities.Comment>(null));
                var handler = new DeleteCommentCommandHandler(unitOfWorkSubstitute);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }

       
        [Fact]
        public async Task DeleteComment_WhenExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteCommentCommand
                {
                    Id = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.CommentRepository.GetById(command.Id).Returns(Task.FromResult<BRO.Domain.Entities.Comment>(sut.CreateComment("Super produkt")));
                var handler = new DeleteCommentCommandHandler(unitOfWorkSubstitute);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeTrue();
            }
        }
    }
}
