using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace Presentation
{
    [ApiController]
    [Route(template: "api/[controller]")]
    public class ProductController (IServicesManager _servicesManager):ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {

            var result = await _servicesManager.productServices.GetAllProductsAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")] // GET: /api/products/12
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _servicesManager.productServices.GetProductByIdAsync(id);
            if (result is null) return NotFound(); // 404
            return Ok(result);
        }

        [HttpGet]
        [Route("Brands")]
        public async Task<IActionResult> GetAllBrands()
        {

            var result = await _servicesManager.productServices.GetAllBrandsAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpGet]
        [Route("Types")]
        public async Task<IActionResult> GetAllTypes()
        {

            var result = await _servicesManager.productServices.GetAl1TypesAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }
    }
}
