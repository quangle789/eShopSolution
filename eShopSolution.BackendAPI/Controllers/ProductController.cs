using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application.CataLog.Products;
using eShopSolution.Application.Common;
using eShopSolution.ViewModels.CataLog.Product;
using eShopSolution.ViewModels.CataLog.ProductImages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;
        private readonly IStorageService _storageService;
        public ProductController(IProductService ProductService, IStorageService storageService)
        {
            _ProductService = ProductService;
            _storageService = storageService;
        }

        [HttpGet("{productId}/{LanguageId}")]
        public async Task<IActionResult> GetById(int productId, string LanguageId)
        {
            var products = await _ProductService.GetById(productId, LanguageId);
            if(products == null)
            {
                return BadRequest("Cannot find products");
            }
            return Ok(products);
        }

        [HttpGet("{LanguageId}")]
        public async Task<IActionResult> GetAllByCategoryId(string LangueId,[FromQuery]GetPublicProductPagingRequest request)
        {
            var products = await _ProductService.GetAllByCategoryId(LangueId,request);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var results = await _ProductService.Create(request);
            if(results == 0)
            {
                return BadRequest();
            }

            var product = await _ProductService.GetById(results,request.LanguageId);
            return CreatedAtAction(nameof(GetById), new { id = results},results);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var affected = await _ProductService.Update(request);
            if (affected == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> Delete(int ProductId)
        {
            var affected = await _ProductService.Delete(ProductId);
            if (affected == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPatch("{id}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int id, decimal newPrice)
        {
            var isSucuess = await _ProductService.UpdatePrice(id, newPrice);
            if (isSucuess)
            {
                return Ok();
            }
            return BadRequest();
        }

        //Image
        [HttpPost("{productId}/Images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageID = await _ProductService.AddImage(productId, request);
            if (imageID == 0)
            {
                return BadRequest();
            }

            var image = await _ProductService.GetImageById(imageID);

            return CreatedAtAction(nameof(GetImageById), new { id = imageID }, image);
        }

        [HttpGet("{productId}/Images/{ImageId}")]
        public async Task<IActionResult> GetImageById(int ImageId)
        {
            var products = await _ProductService.GetImageById(ImageId);
            if (products == null)
            {
                return BadRequest("Cannot find products");
            }
            return Ok(products);
        }

        [HttpPut("{productId}/images/{imageID}")]
        public async Task<IActionResult> UpdateImages(int imageID, [FromForm] ProductImageUpdateImageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _ProductService.UpdateImage(imageID,request);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{productId}/images/{imageID}")]
        public async Task<IActionResult> RomveImage(int imageID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _ProductService.RemoveImage(imageID);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
