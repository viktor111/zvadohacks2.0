﻿using Euroins.Payment.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using ZvadoHacks.Data;
using ZvadoHacks.Data.Entities;
using ZvadoHacks.Data.Repositories;
using ZvadoHacks.Services;

namespace ZvadoHacks.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<AppDbContext>(opt
                => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return serviceCollection;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IJwtTokenGeneratorService, JwtTokenGeneratorService>();
            serviceCollection.AddScoped<IPasswordManagerService, PasswordManagerService>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IPortScannerService, PortScannerService>();

            return serviceCollection;
        }

        public static IServiceCollection ConfigureRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRepository<BlogPost>, BlogPostRepository>();
            serviceCollection.AddScoped<IRepository<User>, UserRepository>();
            serviceCollection.AddScoped<IRepository<Scan>, ScanRepository>();

            return serviceCollection;
        }
    }
}
