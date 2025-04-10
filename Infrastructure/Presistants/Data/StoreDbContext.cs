using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Presistants.Data
{
    public class StoreDbContext :DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options) 
        {
            
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }

        public DbSet<ProductBrand> ProductBrand { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReferance).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
