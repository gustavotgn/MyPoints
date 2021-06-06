using MyPoints.CommandContract.Interfaces;

namespace MyPoints.Catalog.Domain.Commands.Input
{
    public class RegisterOrderAddresAddressCommand : ICommand
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Number { get; set; }
        public string Complements { get; set; }
        public void Validate()
        {
        }
    }
}