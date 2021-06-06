using Flunt.Notifications;
using MyPoints.Account.Data.Interfaces;
using MyPoints.Account.Domain.Commands.Input;
using MyPoints.Account.Domain.Commands.Output;
using System.Threading;
using System.Threading.Tasks;
using MyPoints.CommandContract.Interfaces;
using MyPoints.CommandContract.Entities;

namespace MyPoints.Account.Domain.Handlers
{
    public class AddAccountHandler : Notifiable, 
        IHandler<AddAccountCommand, AddAccountCommandResult>
    {
        private readonly IAccountContext _context;
        private readonly IMessageService _message;

        public AddAccountHandler(IAccountContext context, IMessageService message)
        {
            _context = context;
            _message = message;
        }

        public async Task<ResultWithData<AddAccountCommandResult>> Handle(AddAccountCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Invalid)
            {
                return ResultWithData<AddAccountCommandResult>.Failed(request.Notifications);
            }
            var account = await _context.Account.GetByUserIdAsync(request.UserId);

            if (account != null)
            {
                AddNotification(new Notification("Account", "Account already registered"));
                return ResultWithData<AddAccountCommandResult>.Success(null);

            }

            var result = await _context.Account.AddAsync(request);

            return ResultWithData<AddAccountCommandResult>.Success(result);
        }

        
    }
}
