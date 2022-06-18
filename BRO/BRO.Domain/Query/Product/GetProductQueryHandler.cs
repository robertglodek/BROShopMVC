using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Product;
using BRO.Domain.Query.DTO.Taste;
using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Product
{
    public class GetProductQueryHandler:IQueryHandler<GetProductQuery,ProductDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetProductQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ProductDTO> HandleAsync(GetProductQuery query)
        {
            var product = await _unitOfWork.ProductRepository.GetById(query.Id);
            if (product== null)
                throw new NotFoundException($"Product with Id: {query.Id} does not exist");
            var productDTO= _mapper.Map<ProductDTO>(product);
            return productDTO;
        }
    }
}
