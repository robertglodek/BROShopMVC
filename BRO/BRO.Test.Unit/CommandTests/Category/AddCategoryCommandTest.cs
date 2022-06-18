using AutoMapper;
using BRO.Domain;
using BRO.Domain.Command.Category;
using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Domain.Mappings;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BRO.Test.Unit.CommandTests.Category
{
  
    public class AddCategoryCommandTest
    {
        [Fact]
        public async Task AddCategory_WhenNotExist_ShouldSuccess()
        {
            using (var sut=new SystemUnderTest())
            {
                var command = new AddCategoryCommand
                {               
                    Name="Białko"
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();


                unitOfWorkSubstitute.CategoryRepository.GetByName(command.Name).Returns(Task.FromResult<BRO.Domain.Entities.Category>(null));
                var handler = new AddCategoryCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeTrue();
            }
        }

        [Fact]
        public async Task AddCategory_WhenExist_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddCategoryCommand
                {
                    Name = "Białko"
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                unitOfWorkSubstitute.CategoryRepository.GetByName(command.Name).Returns(Task.FromResult(sut.CreateCategory("Białko")));
                var handler = new AddCategoryCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().Be(false);
            }
        }
        [Fact]
        public async Task AddCategory_WhenValidationNameNull_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddCategoryCommand { };        
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                var handler = new AddCategoryCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }
        [Fact]
        public async Task AddCategory_WhenValidationNameTooLong_ShouldFail()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new AddCategoryCommand
                {
                    Name = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
                };
                var unitOfWorkSubstitute = Substitute.For<IUnitOfWork>();
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(ApplicationUserProfile).Assembly);

                }).CreateMapper();
                var handler = new AddCategoryCommandHandler(unitOfWorkSubstitute, mapper);
                var result = await handler.HandleAsync(command);
                result.IsSuccess.Should().BeFalse();
            }
        }



       
    }
}
