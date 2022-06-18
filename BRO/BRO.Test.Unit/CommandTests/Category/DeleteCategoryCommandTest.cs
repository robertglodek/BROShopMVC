using BRO.Domain.Command.Category;
using BRO.Domain.IRepositories;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BRO.Test.Unit.CommandTests.Category
{
    public class DeleteCategoryCommandTest
    {
        [Fact]
        public async Task DeleteCategory_WhenNotExist_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteCategoryCommand
                {
                    Id= Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.CategoryRepository.GetById(command.Id).Returns(Task.FromResult<BRO.Domain.Entities.Category>(null));
                var handler = new DeleteCategoryCommandHandler(unitOfWorkSubstitute);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }

        [Fact]
        public async Task DeleteCategory_WhenProductsExist_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteCategoryCommand
                {
                    Id = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var productToReturn =
                unitOfWorkSubstitute.CategoryRepository.GetById(command.Id).Returns(Task.FromResult<BRO.Domain.Entities.Category>(sut.CreateCategoryWithProducs("Białko")));
                var handler = new DeleteCategoryCommandHandler(unitOfWorkSubstitute);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
        [Fact]
        public async Task DeleteCategory_WhenExistsAndNoProducts_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteCategoryCommand
                {
                    Id = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.CategoryRepository.GetById(command.Id,"Products").Returns(Task.FromResult<BRO.Domain.Entities.Category>(sut.CreateCategory("Białko")));
                var handler = new DeleteCategoryCommandHandler(unitOfWorkSubstitute);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeTrue();
            }
        }
    }
}
