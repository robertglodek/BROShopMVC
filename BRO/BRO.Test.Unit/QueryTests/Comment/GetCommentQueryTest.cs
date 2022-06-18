using AutoMapper;
using BRO.Domain;
using BRO.Domain.IRepositories;
using BRO.Domain.Mappings;
using BRO.Domain.Query.Comment;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BRO.Test.Unit.QueryTests.Comment
{
    public class GetCommentQueryTest
    {
        [Fact]
        public async Task GetComment_WhenExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var query = new GetCommentQuery() { Id = Guid.NewGuid() };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.CommentRepository.GetById(query.Id, "User,Product").Returns(Task.FromResult<BRO.Domain.Entities.Comment>(sut.CreateComment("Super produkt")));
                var queryHandler = new GetCommentQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);
                result.Content.Should().Be("Super produkt");
            }
        }
    }
}
