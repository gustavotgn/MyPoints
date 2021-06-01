using MyPoints.Identity.Domain.Interfaces;

namespace MyPoints.Identity.Domain.Commands.Output
{
    public class AddUserCommandResult: ICommandResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}