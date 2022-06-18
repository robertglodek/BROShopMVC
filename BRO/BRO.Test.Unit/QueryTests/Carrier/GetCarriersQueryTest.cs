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
    public class GetCarriersQueryTest
    {
        [Fact]
        public async Task GetCarriers_WhenItsExist_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var carrier = sut.CreateCarrier("Poczta polska");
                IEnumerable<BRO.Domain.Entities.Carrier> carriers = new List<BRO.Domain.Entities.Carrier> { carrier };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                var query = new GetCarriersQuery();
                unitOfWorkSubstitute.CarrierRepository.GetAllAsync().Returns(Task.FromResult<IEnumerable<BRO.Domain.Entities.Carrier>>(carriers));
                var queryHandler = new GetCarriersQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);
                result[0].Name.Should().Be("Poczta polska");
            }
        }

        [Fact]
        public async Task GetCarriers_WhenItsExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {  
                var carrier = sut.CreateCarrier("Poczta polska");
                IEnumerable<BRO.Domain.Entities.Carrier> carriers = new List<BRO.Domain.Entities.Carrier> { carrier };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.CarrierRepository.GetAllAsync().Returns(Task.FromResult<IEnumerable<BRO.Domain.Entities.Carrier>>(carriers));
                var query = new GetCarriersQuery();
                var queryHandler = new GetCarriersQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);
                result.Count.Should().Be(1);
            }
        }

        [Fact]
        public async Task GetCarriers_WhenNotExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {

                var carrier = sut.CreateCarrier("Poczta polska");
                IEnumerable<BRO.Domain.Entities.Carrier> carriers = new List<BRO.Domain.Entities.Carrier> { carrier };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.CarrierRepository.GetAllAsync().Returns(Task.FromResult<IEnumerable<BRO.Domain.Entities.Carrier>>(null));
                var query = new GetCarriersQuery();
                var queryHandler = new GetCarriersQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);
          
                result.Count.Should().Be(0);

            }
        }
    }
}
