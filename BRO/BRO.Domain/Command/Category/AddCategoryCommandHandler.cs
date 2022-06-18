
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
    public class AddCategoryCommandHandler:ICommandHandler<AddCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddCategoryCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(AddCategoryCommand command)
        {
            var validationResult = await new AddCategoryCommandValidator().ValidateAsync(command);
            if(!validationResult.IsValid)
                return Result.Fail(validationResult);
            var categoryExists = await _unitOfWork.CategoryRepository.GetByName(command.Name) is  null;
            if(!categoryExists)
                return Result.Fail("Kategoria o tej nazwie już istnieje");
            var category = _mapper.Map<Entities.Category>(command);
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }  
    }
}
