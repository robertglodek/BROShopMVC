using AutoMapper;
using BRO.Domain.Command.Product;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command.ProductTaste
{
    public class EditProductQuantityCommandHandler : ICommandHandler<EditProductQuantityCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EditProductQuantityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(EditProductQuantityCommand command)
        {
            var validationResult = await new EditProductQuantityCommandValidator().ValidateAsync(command);
            if (validationResult.IsValid == false)
                return Result.Fail(validationResult);
            var product = await _unitOfWork.ProductRepository.GetById(command.ProductId);
            if (product == null)
                return Result.Fail("Produkt nie istnieje");
            if (product.ConcurrencyStamp!=command.ProductConcurrencyStamp)
                return Result.Fail("Praca na nieaktualnych danych");
            var productTastes=_mapper.Map<List<BRO.Domain.Entities.ProductTaste>>(command.ProductTastes);
            await _unitOfWork.ProductTasteRepository.UpdateRangeAsync(productTastes);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
