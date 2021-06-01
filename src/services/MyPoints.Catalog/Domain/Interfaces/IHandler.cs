using MediatR;
using MyPoints.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Interfaces
{
    public interface IHandler<TCommand, TCommandResult>
            : IRequestHandler<TCommand, ResultWithData<TCommandResult>>
                where TCommand : ICommand<TCommandResult>
                where TCommandResult : ICommandResult
    {

    }
}
