
using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.Command.Category
{
    public class EditCategoryCommandHandler:ICommandHandler<EditCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditCategoryCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> HandleAsync(EditCategoryCommand command)
        {
            var validationResult = await new EditCategoryCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            var category = await _unitOfWork.CategoryRepository.GetById(command.Id);
            if (category== null)
                return Result.Fail("Kategoria nie istnieje.");
            var categoryExists = await _unitOfWork.CategoryRepository.CheckIfNameAlreadyExists(command.Id, command.Name);
            if (categoryExists)
                return Result.Fail("Kategoria o tej nazwie już istnieje");
            _mapper.Map(command, category);
            await _unitOfWork.CategoryRepository.UpdateAsync(category);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }

    }
}
