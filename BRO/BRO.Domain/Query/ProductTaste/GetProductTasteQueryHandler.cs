using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.ProductTaste;
using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.ProductTaste
{
    public class GetProductTasteQueryHandler : IQueryHandler<GetProductTasteQuery, ProductTasteDTO>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetProductTasteQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ProductTasteDTO> HandleAsync(GetProductTasteQuery query)
        {
            var productTaste = await _unitOfWork.ProductTasteRepository.GetById(query.Id);
            if (productTaste == null)
                throw new NotFoundException($"ProductTaste with Id: {query.Id} does not exist");
            var productDTO = _mapper.Map<ProductTasteDTO>(productTaste);
            return productDTO;
        }
    }
}
