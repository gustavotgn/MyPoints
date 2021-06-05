using MyPoints.CommandContract.Interfaces;

namespace MyPoints.Account.Domain.Commands.Output
{
    public class AddAccountCommandResult : ICommandResult
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }

    }
}
