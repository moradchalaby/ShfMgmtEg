using Microsoft.AspNetCore.Mvc;

namespace ShfMgmtEg.Mvc.Controllers;

public class AuthController : Controller
{
    // GET
    public IActionResult Login()
    {
        return View();
    }
}