using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ShfMgmtEg.Core.Dtos.User;
using ShfMgmtEg.Core.Response;
using ShfMgmtEg.Service.AuthService;

namespace ShfMgmtEgApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IValidator<LoginUser> _validatorLogin;
    private readonly IValidator<RegisterUser> _validatorRegister;


    public AuthController(IAuthService authService, IValidator<RegisterUser> validatorRegister,
        IValidator<LoginUser> validatorLogin)
    {
        _authService = authService;
        _validatorRegister = validatorRegister;
        _validatorLogin = validatorLogin;
    }


    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterUser user)
    {
        var validationResult = await _validatorRegister.ValidateAsync(user);
        if (!validationResult.IsValid)
        {
            var result = new ServiceResponse<string>();
            result.Message = "Validation Error";
            result.IsSuccess = false;
            result.Data = JsonSerializer.Serialize(validationResult.Errors);
            result.StatusCode = HttpStatusCode.BadRequest;
            ;
            return BadRequest(result);
        }

        var response = await _authService.Register(user);
        if (!response.IsSuccess) return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUser user)
    {
        var response = await _authService.Login(user);
        if (!response.IsSuccess) return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword(int id, string currentPassword, string newPassword)
    {
        var response = await _authService.ChangePassword(id, currentPassword, newPassword);
        if (!response.IsSuccess) return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword(int id, string newPassword)
    {
        var response = await _authService.ResetPassword(id, newPassword);
        if (!response.IsSuccess) return BadRequest(response);

        return Ok(response);
    }
}