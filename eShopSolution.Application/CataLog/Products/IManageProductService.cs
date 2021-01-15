using eShopSolution.ViewModels.CataLog.Product;
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
        Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        Task<bool> UpdatePrice(int productId, decimal newPrice);
        Task<bool> UpdateStock(int productId, int addedQuantity);
        Task AddViewCount(int productID);
        Task<int> AddImages(int productId, List<IFormFile> files);

        Task<int> RemoveImages(int imageId);
        Task<int> UpdateImages(int imageId, string caption, bool isDefault);
        Task<List<ProductImageViewModel>> getListImages(int productId);
    }
}
