using AutoMapper;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddUserCommand, AddUserCommandResult>();
        }
    }
}
