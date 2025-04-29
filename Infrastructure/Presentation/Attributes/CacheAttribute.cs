using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;

namespace Presentation.Attributes
{
    public class CacheAttribute(int durationinSec) : Attribute , IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetService<IServicesManager>().cachService;

            var cacheKey = GenerateCacheKey(context.HttpContext.Request);

            var result = await cacheService.GetCacheValueAsync(cacheKey);

            if (!string.IsNullOrEmpty(result))
            {
                // Return Response
                context.Result = new ContentResult()
                {

                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                    Content = result
                };
                return;


            }
            var contextResult = await next.Invoke();
            if (contextResult.Result is OkObjectResult okObject)

                await cacheService.SetCacheValueAsync(cacheKey, okObject.Value, TimeSpan.FromSeconds(durationinSec));


        }

// Execute The Endpoint
        private string GenerateCacheKey(HttpRequest request)
        {

            var key = new StringBuilder();
            key.Append(request.Path);

            foreach (var item in request.Query.OrderBy(q => q.Key))
            {
                key.Append(handler: $"|{item.Key}-{item.Value}");
            }
            // /api/Products?typeid=1&Sort=pricedesc&PageIndex=1&PageSize=5
            // /api/Products|typeid-1|Sort-pricedesc|PageIndex-1

            return key.ToString();
        }
        }
}
