using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BRO.Domain.Command.Manufacturer
{
    public class DeleteManufacturerCommandHandler : ICommandHandler<DeleteManufacturerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteManufacturerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandleAsync(DeleteManufacturerCommand command)
        {
            var manufacturer = await _unitOfWork.ManufacturerRepository.GetById(command.Id,"Products");
            if (manufacturer == null)
                return Result.Fail("Producent nie istnieje");
            if (manufacturer.Products.Count > 0)
                return Result.Fail("Nie można usunąć: istnieje powiązanie z produktami");
            await _unitOfWork.ManufacturerRepository.RemoveAsync(manufacturer);
            await _unitOfWork.CommitAsync();
            return Result.Ok();      
        }
    }
}
