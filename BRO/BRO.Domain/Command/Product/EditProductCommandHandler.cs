using AutoMapper;
using BRO.Domain.Entities;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.Command.Product
{
    public class EditProductCommandHandler : ICommandHandler<EditProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EditProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(EditProductCommand command)
        {
            var validationResult = await new EditProductCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
                return Result.Fail(validationResult);
            var product = await _unitOfWork.ProductRepository.GetByIdWithDetails(command.Id);
            if (product == null)
            {
                return Result.Fail("Produkt nie istnieje");
            } 
            if(product.ConcurrencyStamp!=command.ConcurrencyStamp)
                return Result.Fail("Praca na nieaktualnych danych");
            var productTastesToRemove = new List<Guid>();
            foreach (var item in product.ProductTastes)
            {
                if(command.TasteIds.Where(n=>n==item.TasteId).Count()==0)
                {
                    if ((await _unitOfWork.OrderDetailsRepository.GetById(item.Id) == null))
                        productTastesToRemove.Add(item.Id); 
                    else
                        return Result.Fail($"Nie można usunąć smaku: {item.Taste.Name}");
                }     
            }
            foreach (var item in command.TasteIds)
            {
                if(product.ProductTastes.Where(n=>n.TasteId==item).Count()==0)
                    product.ProductTastes.Add(new BRO.Domain.Entities.ProductTaste() { ProductId = command.Id, TasteId = item, InStock = 0 }); 
            }
            foreach (var item in productTastesToRemove)
                await _unitOfWork.ProductTasteRepository.RemoveAsync(item);
            _mapper.Map(command, product);
            product.ConcurrencyStamp = Guid.NewGuid().ToString();
            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
