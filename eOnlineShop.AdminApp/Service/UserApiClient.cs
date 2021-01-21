using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.User;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eOnlineShop.AdminApp.Service
{
    public class UserApiClient : IUserAPI
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _Iconfiguration;
        public UserApiClient(IHttpClientFactory httpClientFatory , IConfiguration Iconfiguration)
        {
            _httpClientFactory = httpClientFatory;
            _Iconfiguration = Iconfiguration;
        }

        public async Task<string> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            var respone = await client.PostAsync("/api/user/AuthenCate", httpContent);
            var token = await respone.Content.ReadAsStringAsync();
            return token;
        }

        public async Task<PageResult<UserViewModel>> GetUserPaging(GetUserPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_Iconfiguration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",request.BearerToken);
            var respone = await client.GetAsync($"/api/user/paging?pageIndex={request.PageIndex}&pageSize={request.PageSize}" +
                $"&Keyword={request.Keyword}");
            var body = await respone.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<PageResult<UserViewModel>>(body);
            return users;
        }
    }
}
