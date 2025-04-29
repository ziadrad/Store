using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Presistants.Data;
using Presistants.identity;

namespace Presistants
{
    public class DBIntializer : IDBIntializer
    {
        private readonly StoreDbContext _context;
        private readonly StoreIdentityDbContext _identityDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DBIntializer(StoreDbContext context, StoreIdentityDbContext identityDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._context = context;
            _identityDbContext = identityDbContext;
            _userManager = userManager;
            this._roleManager = roleManager;
        }

       

        public async Task IntializeDbAsync()
        {
            //  create db and  db pending Migrations done 
            if (_context.Database.GetPendingMigrations().Any())
            {
               await _context.Database.MigrateAsync(); 
            }
            if (!_context.ProductBrand.Any())
            {
                var data = await File.ReadAllTextAsync(@"..\Infrastructure\Presistants\Data\Seeds\brands.json");

                var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(data);

                if (ProductBrands is not null && ProductBrands.Any())
                {
                    await _context.ProductBrand.AddRangeAsync(ProductBrands);
                    await _context.SaveChangesAsync();
                }

            }
            if (!_context.ProductType.Any())
            {
                var data = await File.ReadAllTextAsync(@"..\Infrastructure\Presistants\Data\Seeds\types.json");

                var ProductType = JsonSerializer.Deserialize<List<ProductType>>(data);

                if (ProductType is not null && ProductType.Any())
                {
                    await _context.ProductType.AddRangeAsync(ProductType);
                    await _context.SaveChangesAsync();
                }

            }

            if (!_context.Product.Any())
            {
                var data = await File.ReadAllTextAsync(@"..\Infrastructure\Presistants\Data\Seeds\products.json");

            var products =  JsonSerializer.Deserialize<List<Product>>(data);

                if (products is not null && products.Any())
                {
                   await _context.Product.AddRangeAsync(products);
                   await _context.SaveChangesAsync();
                }

            }
       
        }

        public async Task IntializeIdentityAsync()
        {
            if (_identityDbContext.Database.GetPendingMigrations().Any())
            { 
                await _identityDbContext.Database.MigrateAsync();
            }

            if (!_roleManager.Roles.Any())
{
                await _roleManager.CreateAsync(role: new IdentityRole()

                { Name = "Admin" });



                await _roleManager.CreateAsync(role: new IdentityRole()
                { Name = "SuperAdmin" });
            }
            // Seeding
            if (!_userManager.Users.Any())
{
                var superAdminUser = new AppUser()
                {
                    DisplayName = "Super Admin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "0123456789"
                };
                var adminUser = new AppUser()
                {
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "0123456789"
                };
               await _userManager.CreateAsync(superAdminUser, password: "P@ssW0rd");
                await _userManager.CreateAsync(adminUser, password: "P@ssW0rd");
                await _userManager.AddToRoleAsync(superAdminUser, role: "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, role: "Admin"); 
            }
        }
    }
}
