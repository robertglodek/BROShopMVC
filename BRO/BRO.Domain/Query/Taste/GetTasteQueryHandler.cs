using AutoMapper;
using BRO.Domain.IRepositories;
using BRO.Domain.Query.DTO.Taste;
using BRO.Domain.Utilities.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Query.Taste
{
    public class GetTasteQueryHandler : IQueryHandler<GetTasteQuery, TasteDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetTasteQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TasteDTO> HandleAsync(GetTasteQuery query)
        {
            var taste = await _unitOfWork.TasteRepository.GetById(query.Id);
            if (taste == null)
                throw new NotFoundException($"Taste with Id: {query.Id} does not exist");
            return _mapper.Map<TasteDTO>(taste);
        }
    }
}
