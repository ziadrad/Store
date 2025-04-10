﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Services.Abstraction
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync();

        // Get Product By Id

Task<ProductResultDto> GetProductByIdAsync(int id);

        // Get All Brands
      
Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();

// Get All Types

Task<IEnumerable<TypeResultDto>> GetAl1TypesAsync();
    }
}
