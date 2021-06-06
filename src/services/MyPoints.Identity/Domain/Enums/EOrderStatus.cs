using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Enums
{
    public enum EOrderStatus
    {
        OrderInProcess = 1,
        InvalidOrder = 2,
        MakingPayment = 3,
        PaymentMade = 4,
        PaymentRejected = 5,
        OutForDelivery = 6,
        Delivered = 7
    }
}
