using Domain.Contracts;
using Domain.Entities.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presistants;
using Presistants.identity;
using services;
using Shared.ErrorsModels;
using Store.Api.Middlewares;

namespace Store.Api.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInServices();
            services.AddSwaggerServices();


            services.AddInfrastructureServices(configuration);
            services.AddAppServices();
            services.AddIndetityServices();

            services.ConfigureServices();

            return services;
        }

        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }
        private static IServiceCollection AddIndetityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser,IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;
        }
        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {

                config.InvalidModelStateResponseFactory = (actioncontext) =>
                {
                    var errors = actioncontext.ModelState.Where(m => m.Value.Errors.Any())
                        .Select(m => new ValidationError()
                        {

                            Field = m.Key,
                            Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)
                        });

                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(error: response);

                };
            });

            return services;
        }


        public static async Task<WebApplication> ConfigureMiddlewares(this WebApplication app)
        {

            await app.IntializeDbAsync();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseGlobalErrorHandlingMiddleware();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }


        public static async Task<WebApplication> IntializeDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbintializer = scope.ServiceProvider.GetRequiredService<IDBIntializer>();

            await dbintializer.IntializeDbAsync();
            await dbintializer.IntializeIdentityAsync();
            return app;
        }

        public static WebApplication UseGlobalErrorHandlingMiddleware(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}
