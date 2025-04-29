using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BasketCreateOrUpdateExceptions1():BadRequestException($"Invalid Operation When Create Or Update Basket")
    {
    }
}
