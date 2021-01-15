using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application.CataLog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _IPublicProductService;
        public ProductController(IPublicProductService IPublicProductService)
        {
            _IPublicProductService = IPublicProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _IPublicProductService.GetAll();
            return Ok(products);
        }
    }
}
