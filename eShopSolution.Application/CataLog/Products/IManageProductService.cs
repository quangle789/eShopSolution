using eShopSolution.ViewModels.CataLog.Product;
using eShopSolution.ViewModels.CataLog.ProductImages;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.CataLog.Products
{
    public interface IManageProductService
    {
        //danh cho quan ly san pham
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);
        Task<int> Delete(int id);
        Task<ProductViewModel> GetById(int ProductId, string LangueId);
        Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int addedQuantity);
        Task AddViewCount(int productID);
        Task<int> AddImage(int productId, ProductImageCreateRequest request);

        Task<int> RemoveImage(int imageId);
        Task<int> UpdateImage(int imageId, ProductImageUpdateImageRequest productImage);
        Task<List<ProductImageViewModel>> getListImage(int productId);
        Task<ProductImageViewModel> GetImageById(int ImageId);

    }
}
 