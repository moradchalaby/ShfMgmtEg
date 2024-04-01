using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using ShfMgmtEg.Core.Dtos.Employee;
using ShfMgmtEg.Core.Response;
using ShfMgmtEg.Mvc.Models;
using ShfMgmtEg.Mvc.Services;

namespace ShfMgmtEg.Mvc.Controllers;

[Authorize]
public class EmployeesController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly RequestService _requestService;

    public EmployeesController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, RequestService requestService)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _requestService = requestService;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var response = await _requestService.Get<RestResponse>("Employee");
        if ( response?.isSuccess != null && response?.isSuccess == true)
        {
            List<GetEmployee> model = response?.data?.ToObject<List<GetEmployee>>() ?? "VALUE";
           
            return View(model);
        }
        else
        {
            string? message = response?.message;

            var model = new ServiceResponse<string>();
            model.Message = message;
            model.Data = null;
            return View(model);
        }
    }
    
}