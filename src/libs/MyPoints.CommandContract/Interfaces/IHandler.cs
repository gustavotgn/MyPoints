using MediatR;
using MyPoints.CommandContract.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPoints.CommandContract.Interfaces
{
    public interface IHandler<TCommand, TCommandResult>
            : IRequestHandler<TCommand, ResultWithData<TCommandResult>>
                where TCommand : ICommand<TCommandResult>
                where TCommandResult : ICommandResult
    {

    }
}
