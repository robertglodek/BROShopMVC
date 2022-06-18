using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Product;
using BRO.Domain.Query.DTO.ProductTaste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Product
{
    public class GetBestsellersQueryHandler : IQueryHandler<GetBestsellersQuery, List<ProductTasteDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetBestsellersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ProductTasteDTO>> HandleAsync(GetBestsellersQuery query)
        {
            var products = await _unitOfWork.ProductRepository.GetBestsellersAsync(4);
            return _mapper.Map<List<ProductTasteDTO>>(products);
        }
    }
}
