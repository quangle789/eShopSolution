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
        public async Task<ApiResult<string>> Authencate(LoginRequest login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Đăng nhập không đúng");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FisrtName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, login.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<ApiResult<bool>> Delete(Guid Id)
        {
            var user = await _userManager.FindByIdAsync(Id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("User không tồn tại");
            }
            var result = await _userManager.DeleteAsync(user);
            if(result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Xóa không thành công");
        }

        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserViewModel>("User không tồn tại");
            }
            var userVm = new UserViewModel()
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FisrtName = user.FisrtName,
                DOB = user.DOB,
                Id = user.Id,
                LastName = user.LastName,
                UserName = user.UserName,
            };
            return new ApiSuccessResult<UserViewModel>(userVm);
        }

        public async Task<ApiResult<PageResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request)
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
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                items = data,
            };

            return new ApiSuccessResult<PageResult<UserViewModel>>(pageResult);
        }


        public async Task<ApiResult<bool>> Register(RegisterRequest register)
        {
            var username = await _userManager.FindByNameAsync(register.UserName);
            if(username != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            
            if (await _userManager.FindByEmailAsync(register.Email) != null)
            {
                return new ApiErrorResult<bool>("Email đã tồn tại"); 
            }

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
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công !");
        }

        public async Task<ApiResult<bool>> Update(Guid Id, UserUpdateRequest request)
        {
            if (await _userManager.Users.AnyAsync(x=>x.Email == request.Email && x.Id != Id))
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }

            var user = await _userManager.FindByIdAsync(Id.ToString());

            user.DOB = request.DOB;
            user.Email = request.Email;
            user.FisrtName = request.FisrtName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhập không thành công !");
        }
    }
}
