using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.Manufacturer;
using BRO.Domain.Command.Taste;
using BRO.Domain.IRepositories;
using BRO.Domain.Mappings;
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
    public class AddManufacturerCommandTest
    {
        [Fact]
        public async Task AddManufacturer_WhenNotExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddManufacturerCommand
                {
                    Name = "KFD"
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.ManufacturerRepository.GetByName(command.Name).Returns(Task.FromResult<BRO.Domain.Entities.Manufacturer>(null));
                var handler = new AddManufacturerCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeTrue();
            }
        }


        [Fact]
        public async Task AddManufacturer_WhenExist_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddManufacturerCommand
                {
                    Name = "KFD"
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);
                    
                }).CreateMapper();
                unitOfWorkSubstitute.ManufacturerRepository.GetByName(command.Name).ReturnsForAnyArgs(Task.FromResult(sut.CreateManufacturer("KFD")));
                var handler = new AddManufacturerCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().Be(false);
            }
        }
        [Fact]
        public async Task AddManufacturer_WhenValidationNameNull_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddManufacturerCommand { };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                var handler = new AddManufacturerCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
        [Fact]
        public async Task AddManufacturer_WhenValidationNameTooLong_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddManufacturerCommand
                {
                    Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                var handler = new AddManufacturerCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
    }
}
