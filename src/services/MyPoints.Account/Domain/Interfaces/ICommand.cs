using MediatR;
using MyPoints.Account.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Account.Domain.Interfaces
{
    public interface ICommand<TCommandResult> : IRequest<ResultWithData<TCommandResult>> where TCommandResult : ICommandResult
    {
        void Validate();
    }
}

