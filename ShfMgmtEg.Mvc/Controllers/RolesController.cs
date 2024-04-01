using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShfMgmtEg.Mvc.Controllers;

[Authorize]
public class RolesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}