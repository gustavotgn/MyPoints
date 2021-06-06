using Flunt.Notifications;
using MyPoints.Identity.Domain.Enums;

namespace MyPoints.Identity.Domain.Dtos
{
    public class InvalidOrderDto: Notifiable
    {
        public EOrderStatus StatusId { get; set; }
        public int OrderId { get; set; }

    }
}
