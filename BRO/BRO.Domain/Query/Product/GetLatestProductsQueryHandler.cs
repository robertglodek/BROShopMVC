using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Product
{
   

    public class GetLatestProductsQueryHandler : IQueryHandler<GetLatestProductsQuery, List<ProductDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLatestProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> HandleAsync(GetLatestProductsQuery query)
        {
            var products = await _unitOfWork.ProductRepository.GetLatestAsync(4);
            return _mapper.Map<List<ProductDTO>>(products);
        }
    }

}
