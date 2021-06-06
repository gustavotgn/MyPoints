using MyPoints.Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Dtos
{
    public class UpdateOrderStatusDto
    {
        public int OrderId { get; set; }
        public EOrderStatus StatusId { get; set; }
    }
}
