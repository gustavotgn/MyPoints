using MediatR;
using MyPoints.Account.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Account.Domain.Interfaces
{
    public interface IHandler<TCommand, TCommandResult>
            : IRequestHandler<TCommand, ResultWithData<TCommandResult>>
                where TCommand : ICommand<TCommandResult>
                where TCommandResult : ICommandResult
    {

    }
}
