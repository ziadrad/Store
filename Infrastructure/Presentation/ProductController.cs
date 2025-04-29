using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using Shared.ErrorsModels;

namespace Presentation
{
    [ApiController]
    [Route(template: "api/[controller]")]
    public class ProductController (IServicesManager _servicesManager):ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResponse<ProductResultDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<PaginatedResponse<ProductResultDto>>> GetAllProducts([FromQuery] ProductSpecificationParamter productSpecificationParamter)
        {

            var result = await _servicesManager.ProductServices.GetAllProductsAsync( productSpecificationParamter);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpGet("{id}")] // GET: /api/products/12
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResultDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]

        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
        {
            var result = await _servicesManager.ProductServices.GetProductByIdAsync(id);
            if (result is null) throw new ProductNotFoundExceptions(id); // 404
            return Ok(result);
        }

        [HttpGet]
        [Route("Brands")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandResultDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
        {

            var result = await _servicesManager.ProductServices.GetAllBrandsAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }
        [HttpGet]
        [Route("Types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeResultDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<TypeResultDto>> GetAllTypes()
        {

            var result = await _servicesManager.ProductServices.GetAl1TypesAsync();
            if (result == null) return BadRequest();
            return Ok(result);
        }
    }
}
