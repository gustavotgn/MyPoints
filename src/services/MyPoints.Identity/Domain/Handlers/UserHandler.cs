﻿using Flunt.Notifications;
using MyPoints.Identity.Data.Interfaces;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Entities;
using MyPoints.Identity.Domain.Interfaces;
using MyPoints.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Handlers
{
    public class UserHandler : Notifiable,
        IHandler<AddUserCommand, AddUserCommandResult>,
        IHandler<AddUserAddressCommand, AddUserAddressCommandResult>
    {
        private readonly IIdentityContext _context;
        private readonly IRestService _queue;

        public UserHandler(IIdentityContext context, IRestService queue)
        {
            _context = context;
            _queue = queue;
        }


        public async Task<ResultWithData<AddUserCommandResult>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            int userId = 0;
            string token = null;
            try
            {
                request.Validate();

                if (request.Invalid)
                {
                    return ResultWithData<AddUserCommandResult>.Failed(request.Notifications);
                }

                var registeredUser = await _context.User.GetByEmailAsync(request.Email);
                if (registeredUser != null)
                {
                    AddNotification(new Notification("Email", "E-mail already registered"));
                    userId = registeredUser.Id;
                    token = TokenService.GenerateToken(registeredUser.Id, registeredUser.Name, registeredUser.Email);

                }
                if (Invalid)
                {
                    return ResultWithData<AddUserCommandResult>.Failed(this.Notifications);
                }

                var result = await _context.User.AddAsync(request);

                userId = result.Id;
                result.Token = TokenService.GenerateToken(result.Id, result.Name, result.Email);
                token = result.Token;

                return ResultWithData<AddUserCommandResult>.Success(result);
            }
            finally
            {
                _queue.SendAsync(UrlNames.AddAccount, new { },token: token);

            }

        }

        public async Task<ResultWithData<AddUserAddressCommandResult>> Handle(AddUserAddressCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Invalid)
            {
                return ResultWithData<AddUserAddressCommandResult>.Failed(request.Notifications);
            }

            if (await _context.User.AddressExistsAsync(request.UserId))
            {
                AddNotification(new Notification("User", "This User already has address"));

            }
            if (Invalid)
            {
                return ResultWithData<AddUserAddressCommandResult>.Failed(this.Notifications);
            }

            var result = await _context.Address.AddAsync(request);

            return ResultWithData<AddUserAddressCommandResult>.Success(result);
        }
    }
}
