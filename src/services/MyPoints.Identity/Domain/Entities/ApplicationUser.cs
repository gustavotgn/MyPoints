using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Entities
{
    public partial class ApplicationUser : IdentityUser<Guid>, IBaseEntity<Guid>
    {
        public UserAddress Address { get; set; }
        public bool IsExcluded { get; set; }
        public Guid? CreatedUser { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
