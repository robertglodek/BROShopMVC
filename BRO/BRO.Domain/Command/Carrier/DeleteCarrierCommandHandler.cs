using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRO.Domain.IRepositories;
using BRO.Domain.Utilities;

namespace BRO.Domain.Command.Carrier
{
    public class DeleteCarrierCommandHandler : ICommandHandler<DeleteCarrierCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCarrierCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandleAsync(DeleteCarrierCommand command)
        {
            var carrier = await _unitOfWork.CarrierRepository.GetById(command.Id,"Orders");
            if (carrier == null)
                return Result.Fail("Dostawca nie istnieje");
            if (carrier.Orders.Count > 0)
                return Result.Fail("Nie można usunąć: istnieje powiązanie z zamówieniami");
            await _unitOfWork.CarrierRepository.RemoveAsync(carrier);
            await _unitOfWork.CommitAsync();
            return Result.Ok();
        }
    }
}
