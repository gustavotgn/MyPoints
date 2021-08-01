using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using MyPoints.CommandContract.Entities;
using MyPoints.CommandContract.Interfaces;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Entities;
using MyPoints.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Handlers
{
    public class LoginHandler : Notifiable,
        IHandler<LoginCommand, LoginCommandResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;


        public LoginHandler(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        public async Task<ResultWithData<LoginCommandResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Invalid)
            {
                return ResultWithData<LoginCommandResult>.Failed(request.Notifications);
            }
            var user = await _userManager.FindByEmailAsync(request.Email);

            LoginCommandResult result = new LoginCommandResult();

            if (result is null)
            {
                AddNotification(new Notification("User", "User NotFound"));

            }
            if (Invalid)
            {
                return ResultWithData<LoginCommandResult>.Failed(this.Notifications);
            }

            result.Token = TokenService.GenerateToken(result.Id, result.Name, result.Email);

            return ResultWithData<LoginCommandResult>.Success(result);
        }
    }
}
