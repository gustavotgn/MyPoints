using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyPoints.Identity.Test.Services
{
    public class MediatorFake : IMediator
    {
        public async Task Publish(object notification, CancellationToken cancellationToken = default)
        {
        }

        public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {

        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var mapper = AutoMapperFake.Get();

            return mapper.Map<TResponse>(request);
        }

        public async Task<object> Send(object request, CancellationToken cancellationToken = default)
        {

            return null;
        }
    }
}
