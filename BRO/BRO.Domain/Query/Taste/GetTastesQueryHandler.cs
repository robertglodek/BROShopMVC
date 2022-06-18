using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Category;
using BRO.Domain.Query.DTO.Taste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Taste
{
    public class GetTastesQueryHandler : IQueryHandler<GetTastesQuery, List<TasteDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTastesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<TasteDTO>> HandleAsync(GetTastesQuery query)
        {
            var tastes = await _unitOfWork.TasteRepository.GetAllAsync();
            return _mapper.Map<List<TasteDTO>>(tastes);
        }
    }
}
