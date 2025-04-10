using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstraction;
using Shared;

namespace services
{
    public class ProductService(IUnit_of_work _unit_Of_Work,IMapper _mapper) : IProductServices
    {
    

        public async Task<IEnumerable<TypeResultDto>> GetAl1TypesAsync()
        {
            var products = await _unit_Of_Work.GetRepository<ProductType, int>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<TypeResultDto>>(products);
            return result;
        }

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var products = await _unit_Of_Work.GetRepository<ProductBrand, int>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandResultDto>>(products);
            return result;
        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {

            var products = await _unit_Of_Work.GetRepository<Product, int>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<ProductResultDto>>(products);
            return result;
        }

        public async  Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var product = await _unit_Of_Work.GetRepository<Product, int>().GetAsync(id);
            if (product is null) return null;

            var result = _mapper.Map<ProductResultDto>(product);
            return result;
        }
    }
}
