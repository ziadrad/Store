
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistants;
using Presistants.Data;
using services;
using Services.Abstraction;
using Shared.ErrorsModels;
using Store.Api.Extensions;
using Store.Api.Middlewares;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Store.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.RegisterAllServices(builder.Configuration);

            var app = builder.Build();

            await app.ConfigureMiddlewares();
            app.Run();
        }
    }
}
