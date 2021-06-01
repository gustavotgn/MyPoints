using MediatR;
using MyPoints.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Domain.Interfaces
{
    public interface ICommand<TCommandResult> : IRequest<ResultWithData<TCommandResult>> where TCommandResult : ICommandResult
    {
        void Validate();
    }
}

