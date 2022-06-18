using AutoMapper;
using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.ProductTaste;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ShoppingCart
{
    public class AddShoppingCartItemCommandHandler : ICommandHandler<AddShoppingCartItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddShoppingCartItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(AddShoppingCartItemCommand command)
        {
            var validationResult = await new AddShoppingCartItemCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
                return Result.Fail(validationResult);
            var productTasteInStock = (await _unitOfWork.ProductTasteRepository.GetById(command.ProductTasteId)).InStock;
            var shoppingCartItemFromDb = await _unitOfWork.ShoppingCartItemRepository.GetByUserIdAndProductTasteId(command.ApplicationUserId,command.ProductTasteId);    
            if (shoppingCartItemFromDb == null)
            {
                if(productTasteInStock>=command.Count)
                {
                    ShoppingCartItem shoppingCartItem;
                    shoppingCartItem = _mapper.Map<ShoppingCartItem>(command);
                    await _unitOfWork.ShoppingCartItemRepository.AddAsync(shoppingCartItem);
                }
                else
                    return Result.Fail("Brak produktu o tym smaku w podanej ilości");
            }  
            else
            {
                if(productTasteInStock >= shoppingCartItemFromDb.Count + command.Count)
                {
                    shoppingCartItemFromDb.Count += command.Count;
                    await _unitOfWork.ShoppingCartItemRepository.UpdateAsync(shoppingCartItemFromDb);
                }
                else
                    return Result.Fail("Brak produktu o tym smaku w podanej ilości");
            }
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
