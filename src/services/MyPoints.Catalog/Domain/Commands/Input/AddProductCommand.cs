using Flunt.Notifications;
using Flunt.Validations;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.CommandContract.Interfaces;

namespace MyPoints.Catalog.Domain.Commands.Input
{
    public class AddProductCommand :Notifiable, ICommand<AddProductCommandResult>
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public bool IsActive { get; set; }
        public int Count { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Description,nameof(Description),"Can not be null or empty")
                .IsGreaterThan(Value, 0, nameof(Value), "Needs be more than 0")
                .IsGreaterThan(Count, 0, nameof(Count), "Needs be more than 0")
            );

        }
    }
}
