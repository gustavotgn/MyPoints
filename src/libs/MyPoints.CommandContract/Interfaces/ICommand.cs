using MediatR;
using MyPoints.CommandContract.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPoints.CommandContract.Interfaces
{
    public interface ICommand<TCommandResult> : IRequest<ResultWithData<TCommandResult>> where TCommandResult : ICommandResult
    {
        void Validate();
    }

    public interface ICommand : INotification
    {
        void Validate();
    }
}
