using BRO.Domain.Command;
using BRO.Domain.Query;
using BRO.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain
{
    public interface IMediator
    {
        Task<Result> CommandAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query);
        Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>;
    }
}
