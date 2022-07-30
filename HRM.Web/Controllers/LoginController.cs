using HRM.Models;
using HRM.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace HRM.Web.Controllers
{

    public class LoginController : BaseController 
    {
        private readonly ILogger<LoginController> _logger;
        public LoginController(HttpClient _httpClient, ILogger<LoginController> logger,HttpClient httpClient):base(_httpClient)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl)
        {
            if (IsTokenInvalidOrEmpty())
            {
                return View();
            }
            else 
            if (!string.IsNullOrEmpty(returnUrl))//not working yet
            {
                ViewBag.ReturnUrl = returnUrl;
                return RedirectToRoute(returnUrl);
            }
            else
            {
                return RedirectToAction("Profile","User");
            }
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel login)
        {   
            var result = await _httpClient.PostAsJsonAsync<LoginModel>("api/User/Login", login);
            var response = await result.Content.ReadAsAsync<LoginResponse>();
            if (response.Data != null && result.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(response.Data));
                HttpContext.Session.SetString("token", response.Token);
                if (response.Data.Type==UserType.manager)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return RedirectToAction("Index","Vacation");
                }
            }
            else if(response.Data==null && response.Token==null && response.ErrorCode!=0)
            {
                ModelState.AddModelError(string.Empty, "User not Found ");
                return View();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Falied");
                return View();
            }

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            var user = GetToken();
            return View("Login");
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(UpdatePasswordDto updatePasswordDto)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(" https://localhost:7266/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var putTask = client.PutAsJsonAsync<UpdatePasswordDto>("api/User/ResetPassword", updatePasswordDto);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Reset Successfully");
                    return RedirectToAction("Login", "Login");
                }
                ModelState.AddModelError(string.Empty, "UserNotFound");
                return View();
            }
        }



    }
}