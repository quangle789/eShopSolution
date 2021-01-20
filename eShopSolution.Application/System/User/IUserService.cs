using eShopSolution.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.User
{
    public interface IUserService
    {
        Task<string> Authencate(LoginRequest login);
        Task<bool> Register(RegisterRequest register);
    }
}
