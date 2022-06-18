using AutoMapper;
using BRO.Domain;
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

namespace BRO.Test.Unit.CommandTests.Taste
{
    public class AddTasteCommandTest
    {
        [Fact]
        public async Task AddTaste_WhenNotExist_ShouldSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddTasteCommand
                {
                    Name = "Białko"
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.TasteRepository.GetByName(command.Name).Returns(Task.FromResult<BRO.Domain.Entities.Taste>(null));
                var handler = new AddTasteCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeTrue();
            }
        }





        [Fact]
        public async Task AddTaste_WhenExist_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddTasteCommand
                {
                    Name = "Czekoladowy"
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.TasteRepository.GetByName(command.Name).Returns(Task.FromResult<BRO.Domain.Entities.Taste>(sut.CreateTaste("Czekoladowy")));
                var handler = new AddTasteCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().Be(false);
            }
        }
        [Fact]
        public async Task AddTaste_WhenValidationNameNull_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddTasteCommand { };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                var handler = new AddTasteCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
        [Fact]
        public async Task AddTaste_WhenValidationNameTooLong_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddTasteCommand
                {
                    Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                var handler = new AddTasteCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
    }
}
