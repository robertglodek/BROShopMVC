using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.Command.Product
{
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandleAsync(DeleteProductCommand command)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdWithDetails(command.Id);
            if (product == null)
                return Result.Fail("Produkt nie istnieje");
            foreach (var item in product.ProductTastes)
            {
                if(item.OrderDetails.Count() > 0)
                    return Result.Fail("Nie można usunąć: istnieje powiązanie z zamówieniami");
            }
            await _unitOfWork.ProductRepository.RemoveAsync(product);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
