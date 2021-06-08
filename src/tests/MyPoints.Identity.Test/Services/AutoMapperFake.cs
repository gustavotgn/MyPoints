using AutoMapper;
using MyPoints.Identity.Domain.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPoints.Identity.Test.Services
{
    public class AutoMapperFake
    {

        public static IMapper Get()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(typeof(UserProfile)));
            return config.CreateMapper();
        }
    }
}
