using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;

namespace services
{
    public static class AppServicesRegistration
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AssemblyReferance).Assembly);
            services.AddScoped<IServicesManager, ServiceManager>();


            return services;

        }
    }
}
