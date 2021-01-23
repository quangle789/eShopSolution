using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineShop.AdminApp.Service
{
    public interface IUserAPI
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<ApiResult<PageResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request);
        Task<ApiResult<bool>> RegisterUser(RegisterRequest request);
        Task<ApiResult<bool>> UpdateUser(Guid Id, UserUpdateRequest request);
        Task<ApiResult<UserViewModel>> GetById(Guid Id);
        Task<ApiResult<bool>> Delete(Guid Id);
    }
}
