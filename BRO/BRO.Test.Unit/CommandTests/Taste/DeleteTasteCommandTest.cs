using BRO.Domain.Command.Taste;
using BRO.Domain.IRepositories;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BRO.Test.Unit.CommandTests.Taste
{
    public class DeleteTasteCommandTest
    {
        [Fact]
        public async Task DeleteTaste_WhenNotExist_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteTasteCommand
                {
                    Id = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.TasteRepository.GetById(command.Id).Returns(Task.FromResult<BRO.Domain.Entities.Taste>(null));
                var handler = new DeleteTasteCommandHandler(unitOfWorkSubstitute);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }

        [Fact]
        public async Task DeleteTaste_WhenProductsExist_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteTasteCommand
                {
                    Id = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.TasteRepository.GetById(command.Id).Returns(Task.FromResult<BRO.Domain.Entities.Taste>(sut.CreateTasteWithProducTastes("Czekoladowy")));
                var handler = new DeleteTasteCommandHandler(unitOfWorkSubstitute);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
        [Fact]
        public async Task DeleteTaste_WhenExistsAndNoProductsTastes_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new DeleteTasteCommand
                {
                    Id = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                unitOfWorkSubstitute.TasteRepository.GetById(command.Id,"ProductTastes").Returns(Task.FromResult<BRO.Domain.Entities.Taste>(sut.CreateTaste("Czekoladowy")));
                var handler = new DeleteTasteCommandHandler(unitOfWorkSubstitute);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeTrue();
            }
        }
    }
}
