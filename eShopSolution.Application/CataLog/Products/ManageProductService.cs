﻿using EShop.Utilities;
using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.CataLog.Product;
using eShopSolution.ViewModels.CataLog.ProductImages;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.CataLog.Products
{
    public class ManageProductService : IProductService
    {
        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;

        public ManageProductService(EShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task AddViewCount(int productID)
        {
            var product = await _context.Products.FindAsync(productID);
            product.ViewCount += 1;
            await _context.SaveChangesAsync(); 
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details= request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    }
                }
            };
            //save image
            if(request.ThumNialImage != null)
            {
                //var thumNialImages = await _context.ProductImages.FirstOrDefaultAsync(u => u.IsDefualt == true && u.ProductId == request.Id);
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption ="ThumbNail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumNialImage.Length,
                        ImagePath = await this.SaveFile(request.ThumNialImage),
                        IsDefualt = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null)
            {
                throw new EshopException($"Cannot find a product: {id}");
            }
            var thumNialImages = _context.ProductImages.Where(u => u.ProductId == id);
            foreach (var item in thumNialImages)
            {
                await  _storageService.DeleteFileAsync(item.ImagePath);
            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        { 
            //1 select
            var query = from p in _context.Products join pt in _context.ProductTranslations on 
                        p.Id equals pt.ProductId join pic in _context.ProductInCategories on p.Id 
                        equals pic.ProductId join c in _context.Categories on pic.CategoryId equals c.ID
                        select new {p,pt,pic};

            //2 filter
            if(!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));
            }

            if(request.CategoryIDs.Count > 0 )
            {
                query = query.Where(p => request.CategoryIDs.Contains(p.pic.CategoryId));
            }

            //3 paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new ProductViewModel() { 
                Id = x.p.Id,
                Name = x.pt.Name,
                DateCreated = x.p.DateCreated,
                Description = x.pt.Description,
                Details = x.pt.Details,
                LanguageId = x.pt.LanguageId,
                OriginalPrice = x.p.OriginalPrice,
                Price = x.p.Price,
            }).ToListAsync();

            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                items = data,
            };

            return pageResult;
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranlation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);
            if(product == null || productTranlation == null)
            {
                throw new EshopException($"Cannot find a product with id: {request.Id}");
            }
            productTranlation.Name = request.Name;
            productTranlation.SeoAlias = request.SeoAlias;
            productTranlation.SeoDescription = request.SeoDescription;
            productTranlation.SeoTitle = request.SeoTitle;
            productTranlation.Description = request.Description;
            productTranlation.Details = request.Details;
            if (request.ThumNialImage != null)
            {
                var thumNialImages = await _context.ProductImages.FirstOrDefaultAsync(u => u.IsDefualt == true && u.ProductId == request.Id);
                if(thumNialImages != null)
                {
                    thumNialImages.FileSize = request.ThumNialImage.Length;
                    thumNialImages.ImagePath = await this.SaveFile(request.ThumNialImage);
                    _context.ProductImages.Update(thumNialImages);                
                }
            }
            return await _context.SaveChangesAsync(); 
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new EshopException($"Cannot find a product with id: {productId}");
            }

            product.Price += newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new EshopException($"Cannot find a product with id: {productId}");
            }

            product.Price += addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition)
                .FileName.Trim('"');
            var filename = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), filename);
            return filename;
        }

        public async Task<ProductViewModel> GetById(int ProductId, string LangueId)
        {
            var product = await _context.Products.FindAsync(ProductId);
            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == ProductId && x.LanguageId == LangueId) ;
            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                DateCreated = product.DateCreated,
                Description = productTranslation != null ? productTranslation.Description : null,
                LanguageId = productTranslation.LanguageId,
                Details = productTranslation != null ? productTranslation.Name : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = productTranslation != null ? productTranslation.SeoAlias : null,
                SeoDescription = productTranslation != null ? productTranslation.SeoDescription : null,
                SeoTitle = productTranslation != null ? productTranslation.SeoTitle :null,
                Stock = product.Stock,
                ViewCount = product.ViewCount
            };
            return productViewModel;
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest productImage)
        {
            var product = new ProductImage()
            {
                Caption = productImage.Caption,
                DateCreated = DateTime.Now,
                IsDefualt = productImage.IsDefualt,
                ProductId = productId,
                SortOrder = productImage.SortOrder
            };
            //save Image
            if (productImage.ImageFile != null)
            {
                product.ImagePath = await this.SaveFile(productImage.ImageFile);
                product.FileSize = productImage.ImageFile.Length;
            }
            _context.ProductImages.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if(productImage == null)
            {
                throw new EshopException($"Cannot find an image with Id {imageId}");
            }

            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateImageRequest productImage)
        {
            var product = await _context.ProductImages.FindAsync(imageId);
            if(product == null)
            {
                throw new EshopException("Cannot Find Image");
            }

            //save Image
            if (productImage.ImageFile != null)
            {
                product.ImagePath = await this.SaveFile(productImage.ImageFile);
                product.FileSize = productImage.ImageFile.Length;
            }
            _context.ProductImages.Update(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductImageViewModel>> getListImage(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId).Select(i=>new ProductImageViewModel() { 
                Caption = i.Caption,
                DateCreated = i.DateCreated,
                FileSize = i.FileSize,
                id = i.Id,
                ImagePath = i.ImagePath,
                IsDefualt=i.IsDefualt,
                ProductId = i.ProductId,
                SortOrder = i.SortOrder
            }).ToListAsync();
        }

        public async Task<ProductImageViewModel> GetImageById(int ImageId)
        {
            var image = await _context.ProductImages.FindAsync(ImageId);

            if (image == null)
            {
                throw new EshopException("Cannot Find Image");
            }

            var viewModel = new ProductImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                id = image.Id,
                ImagePath = image.ImagePath,
                IsDefualt = image.IsDefualt,
                ProductId = image.ProductId,
                SortOrder = image.SortOrder
            };

            return viewModel;
        }

        public async Task<PageResult<ProductViewModel>> GetAllByCategoryId(string LangueId, GetPublicProductPagingRequest request)
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on
p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id
equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.ID
                        where pt.LanguageId == LangueId
                        select new { p, pt, pic };

            //2 filter
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }

            //3 paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock
                }).ToListAsync();

            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                items = data,
            };

            return pageResult;
        }
    }
}
