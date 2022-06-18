using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.DiscountCode;
using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.DiscountCode
{
    public class GetDiscountCodeQueryHandler : IQueryHandler<GetDiscountCodeQuery, DiscountCodeDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDiscountCodeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DiscountCodeDTO> HandleAsync(GetDiscountCodeQuery query)
        {
            BRO.Domain.Entities.DiscountCode discountCode;
            if(query.CodeName!=null)
                discountCode = await _unitOfWork.DiscountCodeRepository.GetByName(query.CodeName);
            else
                discountCode = await _unitOfWork.DiscountCodeRepository.GetById(query.Id);
            if (discountCode == null)
                return null;
            return _mapper.Map<DiscountCodeDTO>(discountCode);
        }
    }
}
