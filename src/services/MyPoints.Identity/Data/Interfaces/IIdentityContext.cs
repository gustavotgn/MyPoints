using MyPoints.Identity.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Data.Interfaces
{
    public interface IIdentityContext
    {
        IUserRepository User { get; }
        IAddressRepository Address { get; }
    }
}
