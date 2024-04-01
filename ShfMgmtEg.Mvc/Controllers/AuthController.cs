using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using ShfMgmtEg.Core.Dtos.User;
using ShfMgmtEg.Mvc.Models;
using ShfMgmtEg.Mvc.Services;


namespace ShfMgmtEg.Mvc.Controllers;


public class AuthController : Controller
{
    private IHttpContextAccessor _httpContextAccessor;
    private readonly RequestService _requestService;

    public AuthController(IHttpContextAccessor httpContextAccessor, RequestService requestService)
    {
        _httpContextAccessor = httpContextAccessor;
        _requestService = requestService;
    }
    // GET
    
    public IActionResult Login()
    {
        var model = new LoginModel();
        
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginUser user)
    {
        var response = await _requestService.Post<RestResponse>("Auth/Login", user);
        
        if (response.statusCode == HttpStatusCode.OK)
        {
            string token = response.data.ToString();
            var model = new LoginModel
            {
                UserName = user.UserName,
                Password = user.Password,
                Data = token
            };
            _httpContextAccessor.HttpContext?.Session.SetString("AccessToken", token);

            _ = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "User")
            }, CookieAuthenticationDefaults.AuthenticationScheme)), new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
            });
            return RedirectToAction("Index", "Home", model);
        }
        else
        {

            string message = string.IsNullOrEmpty(response.errors) 
                ? response.message : response.errors?.password || response.errors?.username;
            var model = new LoginModel
            {
                UserName = user.UserName,
                Password = user.Password,
                ErrorMessage = message
            };
            return View(model);
        }

    }
    
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
    
    public IActionResult Register()
    {
        var model = new RegisterModel();
        
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterUser user)
    {
        var response = await _requestService.Post<RestResponse>("Auth/Register", user);

       
            if ( response?.isSuccess != null && response?.isSuccess == true)
            {
                var model = new RegisterModel
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    Data = response?.data?.ToString()
                };
                return RedirectToAction("Login", model);
            }
            else
            {
                string message = response?.message;
                   
                var model = new RegisterModel
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    ErrorMessage = message
                };
                return View(model);
            }
    }
}
