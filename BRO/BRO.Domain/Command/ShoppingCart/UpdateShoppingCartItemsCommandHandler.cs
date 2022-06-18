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
    public class UpdateShoppingCartItemsCommandHandler : ICommandHandler<UpdateShoppingCartItemsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateShoppingCartItemsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(UpdateShoppingCartItemsCommand command)
        {
            var shoppingCartItems= new List<Entities.ShoppingCartItem>();
            var productTastes = await _unitOfWork.ProductTasteRepository.GetAllByIds(command.Items.Select(n => n.ProductTasteId));
            if(productTastes.Count()>0 && productTastes!=null)
            {
                foreach (var item in productTastes)
                {
                    var newItem = new Entities.ShoppingCartItem() { ProductTasteId = item.Id, ApplicationUserId=command.UserId };
                    newItem.Count = command.Items.FirstOrDefault(n => n.ProductTasteId == item.Id).Count;
                    shoppingCartItems.Add(newItem);
                }
            }
            var itemsToDelete = await _unitOfWork.ShoppingCartItemRepository.GetAllByUserIdAsync(command.UserId);
            await _unitOfWork.ShoppingCartItemRepository.RemoveRangeAsync(itemsToDelete);
            await _unitOfWork.ShoppingCartItemRepository.AddRangeAsync(shoppingCartItems);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
