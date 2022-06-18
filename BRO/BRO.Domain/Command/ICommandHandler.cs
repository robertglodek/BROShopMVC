using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Command
{
    public interface ICommandHandler<TCommand> where TCommand:ICommand
    {
         Task<Result> HandleAsync(TCommand command);
    }
}
