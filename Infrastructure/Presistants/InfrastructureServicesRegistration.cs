using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistants.Data;
using Presistants.Repositories;
using StackExchange.Redis;

namespace Presistants
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration) {

            services.AddDbContext<StoreDbContext>(options =>
            {

                //options. UseSq1Server(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
                services.AddScoped<IDBIntializer, DBIntializer>(); // Allow DI For DbInitializer
                services.AddScoped<IUnit_of_work, Unit_of_work>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddSingleton<IConnectionMultiplexer>((serviceprovider) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
            });

                return services;


        }
    }
}
