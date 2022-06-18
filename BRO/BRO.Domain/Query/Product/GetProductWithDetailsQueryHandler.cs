using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Product;
using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Product
{
    public class GetProductWithDetailsQueryHandler : IQueryHandler<GetProductWithDetailsQuery, ProductDetailsDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetProductWithDetailsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ProductDetailsDTO> HandleAsync(GetProductWithDetailsQuery query)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdWithDetails(query.Id);
            if (product == null)
                throw new NotFoundException($"Product with Id: {query.Id} does not exist");
            var productDTO = _mapper.Map<ProductDetailsDTO>(product);
            return productDTO;
        }
    }
}
