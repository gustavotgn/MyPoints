using Flunt.Notifications;
using Flunt.Validations;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Interfaces;
using System.Text.Json.Serialization;

namespace MyPoints.Identity.Domain.Commands.Input
{
    public class AddUserAddressCommand : Notifiable, ICommand<AddUserAddressCommandResult>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Complements { get; set; }
        public string Number { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .HasMinLen(Street, 1, nameof(Street), "Field length cannot be less than 2 characters")
                .HasMaxLen(Street, 255, nameof(Street), "Field length cannot be more than 255 characters")

                .HasMinLen(City, 2, nameof(City), "Field length cannot be less than 2 characters")
                .HasMaxLen(City, 100, nameof(City), "Field length cannot be more than 100 characters")

                .HasLen(State,2, nameof(State), "The length of the field is 2 characters")

                .HasLen(PostalCode,8, nameof(PostalCode), "Can not be null")
            );

            if (Complements != null)
            {
                AddNotifications(new Contract().HasMaxLen(Complements, 255, nameof(Complements), "Field length cannot be more than 255 characters"));
            }
            if (Number != null)
            {
                AddNotifications(new Contract().HasMaxLen(Number, 20, nameof(Number), "Field length cannot be more than 20 characters"));
            }
        }
    }
    
}
