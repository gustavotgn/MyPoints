using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Dtos
{
    public class RegisterOrderAddressDto
    {
        public int OrderId { get; set; }
        public RegisterOrderAddressAddressDto Address { get; set; }
    }
}
