using AutoMapper;
using BRO.Domain;
using BRO.Domain.IRepositories;
using BRO.Domain.Mappings;
using BRO.Domain.Query.Product;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BRO.Test.Unit.QueryTests.Product
{
    public class GetProductQueryTest
    {
        [Fact]
        public async Task GetProduct_WhenExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var query = new GetProductQuery() { Id = Guid.NewGuid() };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.ProductRepository.GetById(query.Id).ReturnsForAnyArgs(Task.FromResult<BRO.Domain.Entities.Product>(sut.CreateProduct("Batoniks")));
                var queryHandler = new GetProductQueryHandler(mapper, unitOfWorkSubstitute);
                var result = await queryHandler.HandleAsync(query);
                result.Name.Should().Be("Batoniks");
            }
        }
    }
}
