using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route(template: "api/[controller]")]
    public class BasketsController (IServicesManager servicesManager):ControllerBase
    {
        [HttpGet] // GET: /api/baskets?id=sadas

        public async Task<IActionResult> GetBasketById(string id)
        {

      
            var result = await servicesManager.basketService.GetBasketAsync(id);
            return Ok(result);

        }


        [HttpPost] // POST: /api/vaskets
        public async Task<IActionResult> UpdateBasket(BasketDto basketDto)
        {
            var result = await servicesManager.basketService.UpdateBasketAsync(basketDto);
            return Ok(result);
        }
        [HttpDelete] // DELETE : /api/baskets?id
       
public async Task<IActionResult> DeleteBasket(string id)
        {

            await servicesManager.basketService.DeleteBasketAsync(id);
            return NoContent(); // 204
        }
    }
}
