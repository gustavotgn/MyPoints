using Flunt.Notifications;
using Flunt.Validations;
using MyPoints.Account.Domain.Commands.Output;
using MyPoints.Account.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
