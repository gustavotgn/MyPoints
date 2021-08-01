using MyPoints.CommandContract.Interfaces;
using System;

namespace MyPoints.Identity.Domain.Commands.Output
{
    public class AddUserCommandResult: ICommandResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}