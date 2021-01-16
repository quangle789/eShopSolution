using eShopSolution.ViewModels.CataLog.Product;
using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.CataLog.Products
{
    public interface IPublicProductService
    { 
        //danh cho ben ngoai goi
        Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);
        Task<List<ProductViewModel>> GetAll(string LanguageId);
    }
}
