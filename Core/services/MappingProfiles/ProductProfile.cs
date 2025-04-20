using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Shared;

namespace services.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product,ProductResultDto>()
                .ForMember(o=>o.BrandName , o=>o.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(o => o.TypeName, o => o.MapFrom(s => s.ProductType.Name)).ForMember(o=>o.PictureUrl,o=>o.MapFrom<PictureUrlResolver>());

            CreateMap<ProductType, TypeResultDto>();
            CreateMap<ProductBrand, BrandResultDto>();

        }
    }
}
