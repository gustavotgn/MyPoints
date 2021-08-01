using Microsoft.AspNetCore.Identity;
using MyPoints.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Data.Seeds
{
    public class SeedData
    {
        internal static void EnsureSeedData(ApplicationDbContext context)
        {

        }

        internal static void EnsureSeedDataDev(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            if (!context.User.Any(x => x.Email == "gustavo.tgn@yopmail.com"))
            {
                var user = new ApplicationUser {
                    Email = "gustavo.tgn@yopmail.com",
                    UserName = "Gustavo",
                    PhoneNumber = "15981574280",
                    CreatedDate = DateTime.UtcNow
                };

                userManager.CreateAsync(user).Wait();
            }
        }
    }
}
