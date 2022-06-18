using BRO.Domain.Command.Carrier;
using BRO.Domain.IRepositories;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BRO.Test.Unit.CommandTests.Carrier
{
    public class DeleteCarrierCommandTest
    {
        [Fact]
        public async Task DeleteCarrier_WhenNotExist_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteCarrierCommand
                {

                     Id= Guid.NewGuid()
                    
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.CarrierRepository.GetById(command.Id).Returns(Task.FromResult<BRO.Domain.Entities.Carrier>(null));
                var handler = new DeleteCarrierCommandHandler(unitOfWorkSubstitute);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }

        [Fact]
        public async Task DeleteCarrier_WhenExistsAndProductsExist_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteCarrierCommand
                {
                    Id = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.CarrierRepository.GetById(command.Id, "Orders").Returns(Task.FromResult<BRO.Domain.Entities.Carrier>(sut.CreateCarrierWithOrders("Poczta polska")));
                var handler = new DeleteCarrierCommandHandler(unitOfWorkSubstitute);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
    }
}
