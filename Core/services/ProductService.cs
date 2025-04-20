using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using services.Specifications;
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

        public async Task<PaginatedResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParamter productSpecificationParamter)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(productSpecificationParamter);
            var specCount = new ProductWithCountSpecification(productSpecificationParamter);
            var products = await _unit_Of_Work.GetRepository<Product, int>().GetAllAsync(spec);
            var count = await _unit_Of_Work.GetRepository<Product, int>().CountAsync(specCount);
            var result = _mapper.Map<IEnumerable<ProductResultDto>>(products);
            return new PaginatedResponse<ProductResultDto>(productSpecificationParamter.PageIndex, productSpecificationParamter.PageSize,count,result);
        }

        public async  Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(id);
            var product = await _unit_Of_Work.GetRepository<Product, int>().GetAsync(spec);
            if (product is null) return null;

            var result = _mapper.Map<ProductResultDto>(product);
            return result;
        }
    }
}
