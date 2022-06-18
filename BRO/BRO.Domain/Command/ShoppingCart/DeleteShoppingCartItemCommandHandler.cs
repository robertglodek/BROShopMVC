using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ShoppingCart
{
    class DeleteShoppingCartItemCommandHandler:ICommandHandler<DeleteShoppingCartItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteShoppingCartItemCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandleAsync(DeleteShoppingCartItemCommand command)
        {
            var shoppingCartItem = await _unitOfWork.ShoppingCartItemRepository.GetById(command.Id);
            if (shoppingCartItem == null)
                return Result.Fail("Produkt w koszyku nie istnieje");
            await _unitOfWork.ShoppingCartItemRepository.RemoveAsync(shoppingCartItem);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }

    }
   
}
