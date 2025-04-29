using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IServicesManager
    {
        IProductServices ProductServices { get; } 
        IBasketService basketService { get; }
        ICachService cachService { get; }
        IAuthService authService { get; }
    }
}
