using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ShoppingCart
{
    public class EditShoppingCartItemCommandHandler:ICommandHandler<EditShoppingCartItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EditShoppingCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(EditShoppingCartItemCommand command)
        {
            var validationResult = await new EditShoppingCartItemCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            var shoppingCartItemFromDb = await _unitOfWork.ShoppingCartItemRepository.GetById(command.ShoppingCartItemId,"ProductTaste");

            if (shoppingCartItemFromDb == null)
                return Result.Fail("Nie znaleziono produktu w koszyku");
            var productTasteInStock = shoppingCartItemFromDb.ProductTaste.InStock;
            if (command.Count > productTasteInStock)
                return Result.Fail($"Brak produktu o tym smaku w podanej ilosci");

            shoppingCartItemFromDb.Count = command.Count;
            await _unitOfWork.ShoppingCartItemRepository.UpdateAsync(shoppingCartItemFromDb);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
