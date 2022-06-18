using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.ProductTaste;
using BRO.Domain.Utilities.CustomExceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.ProductTaste
{
   
    public class GetProductTastesQueryHandler : IQueryHandler<GetProductTastesQuery, List<ProductTasteDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetProductTastesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ProductTasteDTO>> HandleAsync(GetProductTastesQuery query)
        {
            var validationResult = await new GetProductTastesQueryValidator().ValidateAsync(query);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var product = await _unitOfWork.ProductRepository.GetById(query.ProductId);
            if (product == null)
                throw new NotFoundException($"ProductTaste with Id: {query.ProductId} does not exist");
            var productTastes = await _unitOfWork.ProductTasteRepository.GetAllByProductId(query.ProductId);
            return _mapper.Map<List<ProductTasteDTO>>(productTastes);
        }
    }
}
