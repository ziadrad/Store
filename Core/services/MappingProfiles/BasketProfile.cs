using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Services.Abstraction;
using Shared;

namespace services.MappingProfiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
