using Flunt.Notifications;
using Flunt.Validations;
using MyPoints.Account.Domain.Commands.Output;
using MyPoints.CommandContract.Interfaces;
using System.Text.Json.Serialization;

namespace MyPoints.Account.Domain.Commands.Input
{
    public class AddAccountCommand : Notifiable, ICommand<AddAccountCommandResult>
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()

                .IsGreaterThan(UserId,0,nameof(UserId), "Can not be empty")
            );
        }
    }
}
