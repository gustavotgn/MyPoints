namespace MyPoints.Identity.Domain.Queries
{
    public class UserAddressQueryResult
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Number { get; set; }
        public string Complements { get; set; }
    }
}
