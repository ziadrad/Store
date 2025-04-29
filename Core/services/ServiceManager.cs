using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction;

namespace services
{
    public class ServiceManager ( UserManager<AppUser> userManager , IUnit_of_work _unit_Of_Work, IMapper mapper,IBasketRepository basketRepository,ICacheRepository cacheRepository) : IServicesManager
    {
        public IProductServices ProductServices { get; } = new ProductService(_unit_Of_Work,mapper);

        public IBasketService basketService { get; } = new BasketServices(basketRepository, mapper);
        public ICachService cachService { get; } = new CachService(cacheRepository);
        public IAuthService authService { get; } = new AuthService(userManager);
    }
}
