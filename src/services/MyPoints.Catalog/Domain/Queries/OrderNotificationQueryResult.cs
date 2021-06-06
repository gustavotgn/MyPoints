namespace MyPoints.Catalog.Domain.Queries
{
    public class OrderNotificationQueryResult
    {
        public int Id { get; set; }
        public string Property { get; set; }
        public string Message { get; set; }
        internal int OrderId { get; set; }
    }
}