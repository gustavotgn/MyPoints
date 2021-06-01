using MediatR;
using MyPoints.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Interfaces
{
    public interface ICommand<TCommandResult> : IRequest<ResultWithData<TCommandResult>> where TCommandResult : ICommandResult
    {
        void Validate();
    }
}

