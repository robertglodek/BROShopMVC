using AutoMapper;
using BRO.Domain;
using BRO.Domain.IRepositories;
using BRO.Domain.Mappings;
using BRO.Domain.Query.Category;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BRO.Test.Unit.QueryTests.Category
{
    public class GetCategoriesQueryTest
    {

        [Fact]
        public async Task GetCategories_WhenItsExist_ReturnCorrectData()
        {
            using (var sut = new SystemUnderTest())
            { 
                var category = sut.CreateCategory("Białko");
                IEnumerable<BRO.Domain.Entities.Category> categories = new List<BRO.Domain.Entities.Category> { category };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.CategoryRepository.GetAllAsync().Returns(Task.FromResult<IEnumerable<BRO.Domain.Entities.Category>>(categories));
                var query = new GetCategoriesQuery();
                var queryHandler = new GetCategoriesQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);
                result[0].Name.Should().Be("Białko");
            }
        }

        [Fact]
        public async Task GetCategories_WhenItsExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var category = sut.CreateCategory("Białko");
                IEnumerable<BRO.Domain.Entities.Category> categories = new List<BRO.Domain.Entities.Category> { category };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.CategoryRepository.GetAllAsync().Returns(Task.FromResult<IEnumerable<BRO.Domain.Entities.Category>>(categories));
                var query = new GetCategoriesQuery();
                var queryHandler = new GetCategoriesQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);

                result.Count.Should().Be(1);
                
            }
        }

        [Fact]
        public async Task GetCategories_WhenNotExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var category = sut.CreateCategory("Białko");
                IEnumerable<BRO.Domain.Entities.Category> categories = new List<BRO.Domain.Entities.Category> { category };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.CategoryRepository.GetAllAsync().Returns(Task.FromResult<IEnumerable<BRO.Domain.Entities.Category>>(null));
                var query = new GetCategoriesQuery();
                var queryHandler = new GetCategoriesQueryHandler(unitOfWorkSubstitute, mapper);
                var result = await queryHandler.HandleAsync(query);

                result.Count.Should().Be(0);

            }
        }
    }
}
