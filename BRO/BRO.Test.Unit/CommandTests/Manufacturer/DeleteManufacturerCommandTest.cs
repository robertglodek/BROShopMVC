using BRO.Domain.Command.Manufacturer;
using BRO.Domain.IRepositories;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BRO.Test.Unit.CommandTests.Manufacturer
{
    public class DeleteManufacturerCommandTest
    {

        [Fact]
        public async Task DeleteManufacturer_WhenNotExist_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteManufacturerCommand
                {
                    Id = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.ManufacturerRepository.GetById(command.Id).Returns(Task.FromResult<BRO.Domain.Entities.Manufacturer>(null));
                var handler = new DeleteManufacturerCommandHandler(unitOfWorkSubstitute);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
        [Fact]
        public async Task DeleteManufacturer_WhenProductsExist_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteManufacturerCommand
                {
                    Id = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.ManufacturerRepository.GetById(command.Id).Returns(Task.FromResult<BRO.Domain.Entities.Manufacturer>(sut.CreateManufacturerWithProducs("KFD")));
                var handler = new DeleteManufacturerCommandHandler(unitOfWorkSubstitute);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
        [Fact]
        public async Task DeleteManufacturer_WhenExistsAndNoProducts_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteManufacturerCommand
                {
                    Id = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.ManufacturerRepository.GetById(command.Id,"Products").Returns(Task.FromResult<BRO.Domain.Entities.Manufacturer>(sut.CreateManufacturer("KFD")));
                var handler = new DeleteManufacturerCommandHandler(unitOfWorkSubstitute);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeTrue();
            }
        }
    }
}
