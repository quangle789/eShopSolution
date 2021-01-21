using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.System.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleInManager;
        private readonly IConfiguration _config; 
        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleInManager = roleInManager;
            _config = config;
        }
        public async Task<string> Authencate(LoginRequest login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if(user == null)
            {
                return null;
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password,login.RememberMe,true);
            if(!result.Succeeded) 
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name, user.FisrtName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, login.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creads = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"], _config["Tokens:Issuer"],claims,
                expires:DateTime.Now.AddHours(3),signingCredentials:creads);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<PageResult<UserViewModel>> GetUserPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if(!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new UserViewModel()
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    UserName = x.UserName,
                    FisrtName = x.FisrtName,
                    Id = x.Id,
                    LastName = x.LastName
                }).ToListAsync();

            var pageResult = new PageResult<UserViewModel>()
            {
                TotalRecord = totalRow,
                items = data,
            };

            return pageResult;
        }

        public async Task<bool> Register(RegisterRequest register)
        {
            var user = new AppUser()
            {
                DOB = register.DOB,
                Email = register.Email,
                FisrtName = register.FisrtName,
                LastName = register.LastName,
                UserName = register.UserName,
                PhoneNumber = register.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if(result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
