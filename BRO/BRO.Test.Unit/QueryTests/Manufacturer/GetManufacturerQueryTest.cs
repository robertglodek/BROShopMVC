using AutoMapper;
using BRO.Domain;
using BRO.Domain.IRepositories;
using BRO.Domain.Mappings;
using BRO.Domain.Query.Manufacturer;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BRO.Test.Unit.QueryTests.Manufacturer
{
    public class GetManufacturerQueryTest
    {
        [Fact]
        public async Task GetManufacturer_WhenExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var query = new GetManufacturerQuery() { Id = Guid.NewGuid() };
       
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.ManufacturerRepository.GetById(query.Id).Returns(Task.FromResult<BRO.Domain.Entities.Manufacturer>(sut.CreateManufacturer("KFD")));
                var queryHandler = new GetManufacturerQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);
                result.Name.Should().Be("KFD");
            }
        }
    }
}
