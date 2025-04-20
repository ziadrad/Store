using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Shared;

namespace services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecification<Product, int>
    {
        public ProductWithBrandsAndTypesSpecifications(int id) : base(p => p.Id == id)
        {
            applyinclude();
        }
        public ProductWithBrandsAndTypesSpecifications(ProductSpecificationParamter specParams) : base(
            
            P =>
       (string.IsNullOrEmpty(specParams.Search) | P.Name.ToLower().Contains(specParams.Search.ToLower())) &&
(!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId) &&
(!specParams.TypeId.HasValue || P.TypeId == specParams.TypeId))

        {
            applyinclude();

            applySort(specParams.Sort);
            ApplyPagination(specParams.PageIndex, specParams.PageSize);
            }


        protected void applyinclude()
        {
            AddIncludes(P => P.ProductType);
            AddIncludes(P => P.ProductBrand);
        }
        protected void applySort(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {

                    case "namedesc":
                        AddOrderByDescending(P => P.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                        break;

                    case
                "pricedesc":
                        AddOrderByDescending(P => P.Price);
                        break;

                    default:
                        AddOrderBy(P => P.Name);
                        break;

                }

            }
        }
    }
}
