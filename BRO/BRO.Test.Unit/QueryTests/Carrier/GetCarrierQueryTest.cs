using AutoMapper;
using BRO.Domain;
using BRO.Domain.IRepositories;
using BRO.Domain.Mappings;
using BRO.Domain.Query.Carrier;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BRO.Test.Unit.QueryTests.Carrier
{
    public class GetCarrierQueryTest
    {
        [Fact]
        public async Task GetCarier_WhenExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var query = new GetCarrierQuery() { Id=Guid.NewGuid() };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.CarrierRepository.GetById(query.Id).Returns(Task.FromResult<BRO.Domain.Entities.Carrier>(sut.CreateCarrier("Poczta polska")));
                var queryHandler = new GetCarrierQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);
                result.Name.Should().Be("Poczta polska");
            }
        }
    }
}
