using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyPoints.Identity.Data;
using MyPoints.Identity.Domain.Commands.Input;
using MyPoints.Identity.Domain.Commands.Output;
using MyPoints.Identity.Domain.Entities;
using MyPoints.Identity.Domain.Mappers;
using MyPoints.Identity.Repositories;
using MyPoints.Identity.Repositories.Interfaces;
using MyPoints.Message.Integration.Extensions;
using MyPoints.Message.Integration.Settings;
using System;
using System.Text;

namespace MyPoints.Identity.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=sqlserver,1433;Database=Identity.Development;User ID=sa;Password=Password@0101").EnableSensitiveDataLogging());
            services
                .AddDbContext<ApplicationDbContext>(
                    b => b.UseSqlServer("Server=sqlserver,1433;Database=Identity.Development;User ID=sa;Password=Password@0101"))
                .AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.Configure<IdentityOptions>(options =>
            //{
            //     Password settings.
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequiredUniqueChars = 1;

            //     Lockout settings.
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //    options.Lockout.MaxFailedAccessAttempts = 5;
            //    options.Lockout.AllowedForNewUsers = true;

            //     User settings.
            //    options.User.AllowedUserNameCharacters =
            //    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            //    options.User.RequireUniqueEmail = true;
            //});

            //services.ConfigureApplicationCookie(options =>
            //{
            //     Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            //    options.LoginPath = "/Identity/Account/Login";
            //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    options.SlidingExpiration = true;
            //});


            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();

            services.AddHttpContextAccessor();

            services.AddMediatR(typeof(AddUserCommand));
            services.AddAutoMapper(typeof(UserProfile));

            RabbitMQSettings rabbitConfiguration = new RabbitMQSettings();
            configuration.GetSection("RabbitMQConfigurations").Bind(rabbitConfiguration);

            //services.AddHealthChecks()
            //    .AddRabbitMQ(configuration.GetConnectionString("RabbitMQ"), name: "rabbitMQ")
            //services.AddHealthChecksUI();

            services.ConfigureRabbitMQConnection(options =>
            {
                options.HostName = rabbitConfiguration.HostName;
                options.Port = rabbitConfiguration.Port;
                options.UserName = rabbitConfiguration.UserName;
                options.Password = rabbitConfiguration.Password;
            }).AddRabbitMQMessageService()
            .AddRabbiMQConsumer<ValidateOrderAddressCommand, ValidateOrderAddressCommandResult>("validate-order-address");
        }
        public static void AddAuth(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Configurations.Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
