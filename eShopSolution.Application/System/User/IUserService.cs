using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.User
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authencate(LoginRequest login);
        Task<ApiResult<bool>> Register(RegisterRequest register);
        Task<ApiResult<PageResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request);
        Task<ApiResult<bool>> Update(Guid Id, UserUpdateRequest request);
        Task<ApiResult<UserViewModel>> GetById(Guid id);
    }
}
