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
    public class GetManufacturersQueryTest
    {

        [Fact]
        public async Task GetManufacturers_WhenItsExist_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var manufacturer = sut.CreateManufacturer("KFD");
                IEnumerable<BRO.Domain.Entities.Manufacturer> manufacturers = new List<BRO.Domain.Entities.Manufacturer> { manufacturer };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.ManufacturerRepository.GetAllAsync().Returns(Task.FromResult<IEnumerable<BRO.Domain.Entities.Manufacturer>>(manufacturers));
                var query = new GetManufacturersQuery();
                var queryHandler = new GetManufacturersQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);
                result[0].Name.Should().Be("KFD");
            }
        }

        [Fact]
        public async Task GetManufacturers_WhenItsExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {

                var manufacturer = sut.CreateManufacturer("KFD");
                IEnumerable<BRO.Domain.Entities.Manufacturer> manufacturers = new List<BRO.Domain.Entities.Manufacturer> { manufacturer };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.ManufacturerRepository.GetAllAsync().Returns(Task.FromResult<IEnumerable<BRO.Domain.Entities.Manufacturer>>(manufacturers));
                var query = new GetManufacturersQuery();
                var queryHandler = new GetManufacturersQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);
                result.Count.Should().Be(1);

            }
        }

        [Fact]
        public async Task GetManufacturers_WhenNotExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
               
                var manufacturer = sut.CreateManufacturer("KFD");
                IEnumerable<BRO.Domain.Entities.Manufacturer> manufacturers = new List<BRO.Domain.Entities.Manufacturer> { manufacturer };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.ManufacturerRepository.GetAllAsync().Returns(Task.FromResult<IEnumerable<BRO.Domain.Entities.Manufacturer>>(null));
                var query = new GetManufacturersQuery();
                var queryHandler = new GetManufacturersQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);
                result.Count.Should().Be(0);

            }
        }
    }
}
