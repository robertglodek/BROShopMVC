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
    public class AddProductCommandHandler : ICommandHandler<AddProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> HandleAsync(AddProductCommand command)
        {
            var validationResult = await new AddProductCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
                return Result.Fail(validationResult);
            var productExists = await _unitOfWork.ProductRepository.GetByName(command.Name) is null;
            if (!productExists)
                return Result.Fail("Produkt o podanej nazwie już istnieje");
            Guid productId = Guid.NewGuid();
            var productTastes = new List<BRO.Domain.Entities.ProductTaste>();      
            foreach (var id in command.TasteIds)
                productTastes.Add(new BRO.Domain.Entities.ProductTaste() { ProductId = productId, TasteId = id, InStock = 0 });
            var product = _mapper.Map<Entities.Product>(command);
            product.ProductAddDate=DateTimeOffset.Now;
            product.Id = productId;
            product.ProductTastes=productTastes;
            product.ConcurrencyStamp = Guid.NewGuid().ToString();
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
