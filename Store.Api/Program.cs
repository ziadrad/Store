
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistants;
using Presistants.Data;
using services;
using Services.Abstraction;

namespace Store.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>( o =>
            {
                o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
            builder.Services.AddScoped<IUnit_of_work, Unit_of_work>();
            builder.Services.AddAutoMapper(typeof(AssemblyReferance).Assembly);
            builder.Services.AddScoped<IServicesManager, ServiceManager>();
            builder.Services.AddScoped<IDBIntializer,DBIntializer>();

            var app = builder.Build();

          using  var scope = app.Services.CreateScope();
          var dbintializer =  scope.ServiceProvider.GetRequiredService<IDBIntializer>();
           await dbintializer.IntializeDbAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
