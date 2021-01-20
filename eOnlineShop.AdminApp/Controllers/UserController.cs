using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eOnlineShop.AdminApp.Service;
using eShopSolution.ViewModels.System.User;
using Microsoft.AspNetCore.Mvc;

namespace eOnlineShop.AdminApp.Controllers
{

    public class UserController : Controller
    {
        private readonly IUserAPI _userAPI;
        public UserController(IUserAPI userAPI)
        {
            _userAPI = userAPI;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var token = await _userAPI.Authenticate(request);


            return View(token);
        }
    }
}
