using eShopSolution.ViewModels.System.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineShop.AdminApp.Service
{
    public interface IUserAPI
    {
        Task<string> Authenticate(LoginRequest request);

    }
}
