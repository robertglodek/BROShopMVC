using AutoMapper;
using BRO.Domain;
using BRO.Domain.IRepositories;
using BRO.Domain.Mappings;
using BRO.Domain.Query.Taste;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BRO.Test.Unit.QueryTests.Taste
{
    public class GetTasteQueryTest
    {
        [Fact]
        public async Task GetTaste_WhenExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var query = new GetTasteQuery() { Id = Guid.NewGuid() };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.TasteRepository.GetById(query.Id).Returns(Task.FromResult<BRO.Domain.Entities.Taste>(sut.CreateTaste("Czekolaowy")));
                var queryHandler = new GetTasteQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);
                result.Name.Should().Be("Czekolaowy");
            }
        }
    }
}
