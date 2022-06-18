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
    public class GetTastesQueryTest
    {

        [Fact]
        public async Task GetTastes_WhenItsExist_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            {
                var taste = sut.CreateTaste("Czekoladowy");
                IEnumerable<BRO.Domain.Entities.Taste> tastes = new List<BRO.Domain.Entities.Taste> { taste };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.TasteRepository.GetAllAsync().Returns(Task.FromResult<IEnumerable<BRO.Domain.Entities.Taste>>(tastes));
                var query = new GetTastesQuery();
                var queryHandler = new GetTastesQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);
                result[0].Name.Should().Be("Czekoladowy");
            }
        }

        [Fact]
        public async Task GetTastes_WhenItsExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var taste = sut.CreateTaste("Czekoladowy");
                IEnumerable<BRO.Domain.Entities.Taste> tastes = new List<BRO.Domain.Entities.Taste> { taste };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.TasteRepository.GetAllAsync().Returns(Task.FromResult<IEnumerable<BRO.Domain.Entities.Taste>>(tastes));
                var query = new GetTastesQuery();
                var queryHandler = new GetTastesQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);
                result.Count.Should().Be(1);

            }
        }

        [Fact]
        public async Task GetTastes_WhenNotExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
               
                var taste = sut.CreateTaste("Czekoladowy");
                IEnumerable<BRO.Domain.Entities.Taste> tastes = new List<BRO.Domain.Entities.Taste> { taste };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.TasteRepository.GetAllAsync().Returns(Task.FromResult<IEnumerable<BRO.Domain.Entities.Taste>>(null));
                var query = new GetTastesQuery();
                var queryHandler = new GetTastesQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);
                result.Count.Should().Be(0);

            }
        }
    }
}
