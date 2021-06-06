using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyPoints.Account.Configurations;
using MyPoints.Account.Domain.Commands.Input;
using MyPoints.Account.Domain.Commands.Output;
using MyPoints.Message.Integration.Interfaces;
using MyPoints.Message.Integration.Extensions;
using MyPoints.Message.Integration.Settings;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using MyPoints.Account.Data;
using MyPoints.Account.Data.Interfaces;
using MyPoints.Account.Domain.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyPoints.CommandContract.Interfaces;
using MyPoints.Message.Integration.Services;

namespace MyPoints.Account.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<IAccountContext, AccountContext>();
            services.AddHttpContextAccessor();

            services.AddMediatR(typeof(AddAccountCommand));
            services.AddAutoMapper(typeof(AccountProfile));

            services.AddHttpContextAccessor();
            services.AddScoped<ISyncIntegrationService, RestService>();

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
            }).AddRabbitMQMessageService()
            .AddRabbiMQConsumer<AddAccountCommand, AddAccountCommandResult>("register-account")
            .AddRabbiMQConsumer<AddPurchaseTransactionCommand, AddPurchaseTransactionCommandResult>("add-purchase-transaction");
        }
        public static void AddAuth(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
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
