using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyPoints.Catalog.Data;
using MyPoints.Catalog.Data.Interfaces;
using MyPoints.Catalog.Domain.Commands.Input;
using MyPoints.Catalog.Domain.Commands.Output;
using MyPoints.Catalog.Domain.Mappers;
using MyPoints.Message.Integration.Extensions;
using MyPoints.Message.Integration.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPoints.Catalog.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddHttpContextAccessor();

            services.AddMediatR(typeof(AddProductCommand));
            services.AddAutoMapper(typeof(ProductProfile));

            RabbitMQSettings rabbitConfiguration = new RabbitMQSettings();
            configuration.GetSection("RabbitMQConfigurations").Bind(rabbitConfiguration);

            services.AddHealthChecks()
                .AddRabbitMQ(configuration.GetConnectionString("RabbitMQ"), name: "rabbitMQ");
            services.AddHealthChecksUI();

            services.ConfigureRabbitMQConnection(options =>
            {
                options.HostName = rabbitConfiguration.HostName;
                options.Port = rabbitConfiguration.Port;
                options.UserName = rabbitConfiguration.UserName;
                options.Password = rabbitConfiguration.Password;
            }).AddRabbitMQMessageService();
            //.AddRabbiMQConsumer<AddProductCommand, AddProductCommandResult>("register-account");
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
