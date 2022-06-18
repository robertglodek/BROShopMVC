using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;

namespace BRO.Domain.Command.Category
{
    public class DeleteCategoryCommandHandler:ICommandHandler<DeleteCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandleAsync(DeleteCategoryCommand command)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(command.Id,"Products");
            if (category == null)
                return Result.Fail("Kategoria nie istnieje");
            if (category.Products.Count > 0)
                return Result.Fail("Nie można usunąć: istnieje powiązanie z produktami");
            await _unitOfWork.CategoryRepository.RemoveAsync(category);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
