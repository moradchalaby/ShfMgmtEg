using FluentValidation;
using ShfMgmtEg.Core.Dtos.User;
using ShfMgmtEg.Service.UserService;

namespace ShfMgmtEg.Service.Validation.Auth;

public class LoginValidation : AbstractValidator<LoginUser>
{
    private readonly IUserService _userService;

    public LoginValidation()
    {
        RuleFor(x => x.UserName).NotEmpty().MustAsync(ExistUser).WithMessage("Kayıtlı kullanıcı bulunamadı.");
        RuleFor(x => x.Password).NotEmpty();
    }

    private Task<bool> ExistUser(string arg1, CancellationToken arg2)
    {
        return _userService.ExistUser(arg1);
    }
}