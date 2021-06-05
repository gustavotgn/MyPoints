using Flunt.Notifications;
using Flunt.Validations;
using MyPoints.CommandContract.Interfaces;
using MyPoints.Identity.Domain.Commands.Output;

namespace MyPoints.Identity.Domain.Commands.Input
{
    public class AddUserCommand : Notifiable, ICommand<AddUserCommandResult>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .HasMaxLen(Name, 80, nameof(Name), "Name should have no more than 80 chars")
                .HasMinLen(Name, 2, nameof(Name), "Name should have at least 2 chars")

                .IsNotNull(Email, nameof(Email), "Email can not be null")
                .IsEmail(Email, nameof(Email), "Email is invalid")

                //TODO colocar mais validações para senha
                .HasMinLen(Password,6,nameof(Password), "Password should have at least 6 chars")
                .HasMaxLen(Password,20,nameof(Password), "Password should have no more than 20 chars")
            );
        }
    }
}
