using MyPoints.Identity.Data.Interfaces;
using MyPoints.Identity.Repositories.Interfaces;
using MyPoints.Identity.Test.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPoints.Identity.Test.Data
{
    public class FakeIdentityContext : IIdentityContext
    {
        private bool userExists;
        private readonly bool addressExists;

        public FakeIdentityContext(bool userExists = true, bool addressExists = false)
        {
            this.userExists = userExists;
            this.addressExists = addressExists;
        }

        public IUserRepository User => new FakeUserRepository(userExists,addressExists);

        public IAddressRepository Address => new FakeAddressRepository(addressExists);
    }
}
