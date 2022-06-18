using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.Command.Taste
{
    public class DeleteTasteCommandHandler:ICommandHandler<DeleteTasteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteTasteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandleAsync(DeleteTasteCommand command)
        {
            var taste = await _unitOfWork.TasteRepository.GetById(command.Id,"ProductTastes");
            if (taste == null)
                return Result.Fail("Smak nie istnieje");
            if (taste.ProductTastes.Count > 0)
                return Result.Fail("Nie można usunąć: istnieje powiązanie z produktami");
            await _unitOfWork.TasteRepository.RemoveAsync(taste);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }

    }
}
