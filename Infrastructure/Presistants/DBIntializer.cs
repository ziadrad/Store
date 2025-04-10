using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistants.Data;

namespace Presistants
{
    public class DBIntializer : IDBIntializer
    {
        private readonly StoreDbContext _context;

        public DBIntializer(StoreDbContext context)
        {
            this._context = context;
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
    }
}
