using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Entities
{
    public class UserAddress : BaseEntity<Guid>
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Number { get; set; }
        public string Complements { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
