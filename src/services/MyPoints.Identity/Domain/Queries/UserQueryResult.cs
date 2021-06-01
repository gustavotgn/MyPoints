﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Domain.Queries
{
    public class UserQueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserAddressQueryResult Address { get; set; }
    }
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
