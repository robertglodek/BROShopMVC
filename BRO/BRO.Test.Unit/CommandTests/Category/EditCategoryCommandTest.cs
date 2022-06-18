using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.Category;
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

namespace BRO.Test.Unit.CommandTests.Category
{
    public class EditCategoryCommandTest
    {
        [Fact]
        public async Task EditCategory_WhenNotExist_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new EditCategoryCommand
                {
                    Name = "Białko",
                    Id=Guid.NewGuid()
                    
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.CategoryRepository.GetById(command.Id).Returns(Task.FromResult<BRO.Domain.Entities.Category>(null));
               
                var handler = new EditCategoryCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
        [Fact]
        public async Task EditCategory_WhenValidationNameNull_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new EditCategoryCommand { Id = Guid.NewGuid() };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper(); 
                var handler = new EditCategoryCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
        [Fact]
        public async Task EditCategory_WhenValidationNameTooLong_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new EditCategoryCommand
                {
                    Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                    Id = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                var handler = new EditCategoryCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }

        [Fact]
        public async Task EditCategory_WhenNameAlreadyExists_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new EditCategoryCommand
                {
                    Name = "Białko",
                    Id = Guid.NewGuid()
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.CategoryRepository.GetById(command.Id).ReturnsForAnyArgs(Task.FromResult<BRO.Domain.Entities.Category>(sut.CreateCategory("Cytrulina")));
                unitOfWorkSubstitute.CategoryRepository.CheckIfNameAlreadyExists(command.Id, command.Name).Returns(Task.FromResult<bool>(true));
                var handler = new EditCategoryCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
    }

   
}
