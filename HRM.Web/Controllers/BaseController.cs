using HRM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace HRM.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly HttpClient _httpClient;
        public BaseController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(" https://localhost:7266/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpClient httpClients
        {
            get { return _httpClient; }

        }
        public bool IsManager()
        {
            var user = HttpContext.Session.GetString("user");
            if (!string.IsNullOrEmpty(user))
            {
                var result = JsonConvert.DeserializeObject<UserDto>(user);
                if(result!=null && result.Type == UserType.manager)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public UserDto GetUser()
        {
            var user = HttpContext.Session.GetString("user");
            if (!string.IsNullOrEmpty(user))
            {
                var result = JsonConvert.DeserializeObject<UserDto>(user);
                if (result != null)
                {
                    return result;
                }
                return null;
            }
            return null;
        }
        public string GetToken()
        {
            var token = HttpContext.Session.GetString("token");
            if (!string.IsNullOrEmpty(token))
            {
                return token;
            }
            return null;
        }
        public bool IsTokenInvalidOrEmpty()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return true;
            }

            var jwtToken = new JwtSecurityToken(token);
            return (jwtToken == null) || (jwtToken.ValidFrom > DateTime.UtcNow) || (jwtToken.ValidTo < DateTime.UtcNow);
        }
        public void Authorize()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());
        }

    }
}
