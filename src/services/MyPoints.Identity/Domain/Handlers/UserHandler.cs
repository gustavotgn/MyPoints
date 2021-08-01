using AutoMapper;
using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using MyPoints.CommandContract.Entities;
using MyPoints.CommandContract.Interfaces;
using MyPoints.Identity.Data;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Entities;
using MyPoints.Identity.Repositories.Interfaces;
using MyPoints.Identity.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Handlers
{
    public class UserHandler : Notifiable,
        IHandler<AddUserCommand, AddUserCommandResult>,
        IHandler<AddUserAddressCommand, AddUserAddressCommandResult>
    {
        private readonly IMessageService _message;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;

        public UserHandler(IMessageService message, UserManager<ApplicationUser> userManager, IMapper mapper, IAddressRepository addressRepository)
        {
            _message = message;
            _userManager = userManager;
            _mapper = mapper;
            _addressRepository = addressRepository;
        }


        public async Task<ResultWithData<AddUserCommandResult>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            Guid? userId = null;
            string token = null;
            try
            {
                request.Validate();

                if (request.Invalid)
                {
                    return ResultWithData<AddUserCommandResult>.Failed(request.Notifications);
                }

                var registeredUser = await _userManager.FindByEmailAsync(request.Email);
                if (registeredUser != null)
                {
                    AddNotification(new Notification("Email", "E-mail already registered"));
                    userId = registeredUser.Id;
                    token = TokenService.GenerateToken(registeredUser.Id, registeredUser.UserName, registeredUser.Email);

                }
                if (Invalid)
                {
                    return ResultWithData<AddUserCommandResult>.Failed(this.Notifications);
                }

                var createUser = await _userManager.CreateAsync(_mapper.Map<ApplicationUser>(request));

                var result = await _userManager.FindByEmailAsync(request.Email);

                userId = result.Id;
                token = TokenService.GenerateToken(result.Id, result.UserName, result.Email);

                return ResultWithData<AddUserCommandResult>.Success(null);
            }
            finally
            {
                _message.Enqueue("register-account", new { userId });

            }

        }

        public async Task<ResultWithData<AddUserAddressCommandResult>> Handle(AddUserAddressCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (request.Invalid)
            {
                return ResultWithData<AddUserAddressCommandResult>.Failed(request.Notifications);
            }

            if (await _addressRepository.IsExistsByUserId(request.UserId))
            {
                AddNotification(new Notification("User", "This User already has address"));

            }
            if (Invalid)
            {
                return ResultWithData<AddUserAddressCommandResult>.Failed(this.Notifications);
            }
            var entity = _mapper.Map<UserAddress>(request);

            await _addressRepository.InsertAsync(entity);

            return ResultWithData<AddUserAddressCommandResult>.Success(_mapper.Map<AddUserAddressCommandResult>(entity));
        }
    }
}
