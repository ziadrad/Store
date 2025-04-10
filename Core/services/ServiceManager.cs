using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;

namespace services
{
    public class ServiceManager (IUnit_of_work _unit_Of_Work, IMapper mapper) : IServicesManager
    {
        public IProductServices productServices { get; } = new ProductService(_unit_Of_Work,mapper);
    }
}
