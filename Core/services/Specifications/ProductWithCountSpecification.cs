using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Shared;

namespace services.Specifications
{
    public class ProductWithCountSpecification : BaseSpecification<Product, int>
    {
        public ProductWithCountSpecification(ProductSpecificationParamter specParams) : base(
             P =>
       (string.IsNullOrEmpty(specParams.Search) | P.Name.ToLower().Contains(specParams.Search.ToLower())) &&
    (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId) &&
    (!specParams.TypeId.HasValue || P.TypeId == specParams.TypeId))
        {
        }
    }
}
