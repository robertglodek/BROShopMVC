using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.DiscountCode
{
    public class DeleteDiscountCodeCommandHandler : ICommandHandler<DeleteDiscountCodeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteDiscountCodeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandleAsync(DeleteDiscountCodeCommand command)
        {
            var discountCode = await _unitOfWork.DiscountCodeRepository.GetById(command.Id,"Orders");
            if (discountCode == null)
                return Result.Fail("Kod zniżkowy nie istnieje");
            if (discountCode.Orders.Count > 0)
                return Result.Fail("Nie można usunąć tego kodu gdyż istnieją zamówienia z nim powiązane");
            await _unitOfWork.DiscountCodeRepository.RemoveAsync(discountCode);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
