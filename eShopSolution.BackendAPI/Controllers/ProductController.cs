using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application.CataLog.Products;
using eShopSolution.Application.Common;
using eShopSolution.ViewModels.CataLog.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _PublicProductService;
        private readonly IManageProductService _ManageProductService;
        private readonly IStorageService _storageService;
        public ProductController(IPublicProductService PublicProductService, IManageProductService ManageProductService , IStorageService storageService)
        {
            _PublicProductService = PublicProductService;
            _ManageProductService = ManageProductService;
            _storageService = storageService;
        }

        [HttpGet("LanguageId")]
        public async Task<IActionResult> GetAll(string LanguageId)
        {
            var products = await _PublicProductService.GetAll(LanguageId);
            return Ok(products);
        }

        [HttpGet("{productId}/{LanguageId}")]
        public async Task<IActionResult> GetById(int productId, string LanguageId)
        {
            var products = await _ManageProductService.GetById(productId, LanguageId);
            if(products == null)
            {
                return BadRequest("Cannot find products");
            }
            return Ok(products);
        }



        [HttpGet("public-paging/{LanguageId}")]
        public async Task<IActionResult> GetAllByCategoryId([FromQuery]GetPublicProductPagingRequest request)
        {
            var products = await _PublicProductService.GetAllByCategoryId(request);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            var results = await _ManageProductService.Create(request);
            if(results == 0)
            {
                return BadRequest();
            }

            var product = await _ManageProductService.GetById(results,request.LanguageId);
            return CreatedAtAction(nameof(GetById), new { id = results},results);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest request)
        {
            var affected = await _ManageProductService.Update(request);
            if (affected == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> Delete(int ProductId)
        {
            var affected = await _ManageProductService.Delete(ProductId);
            if (affected == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("price/{id}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int id, decimal newPrice)
        {
            var isSucuess = await _ManageProductService.UpdatePrice(id, newPrice);
            if (isSucuess)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
