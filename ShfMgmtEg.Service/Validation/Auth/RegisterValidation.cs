using FluentValidation;
using ShfMgmtEg.Core.Dtos.User;
using ShfMgmtEg.Service.UserService;

namespace ShfMgmtEg.Service.Validation.Auth;

public class RegisterValidation: AbstractValidator<RegisterUser>
{
  private readonly IUserService _userService;
        public RegisterValidation(IUserService userService)
        {
            _userService = userService;
            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(3);
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(3);
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(3).MustAsync(BeUniqueUser).WithMessage("Fartklı bir kullanıcı adı seçiniz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Boş Olamaz.").EmailAddress().WithMessage("Email formatına uygun değil").MustAsync(BeUniqueUser).WithMessage("Email sistemde kayıtlı.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon Numarası Boş Olamaz.").MustAsync(BeUniqueUser).WithMessage("Telefon numarası sistemde kayıtlı.");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalı.");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Şifreler uyuşmuyor.");
        }

        private async Task<bool> BeUniqueUser(string arg1, CancellationToken arg2)
        {
            
            var result = await _userService.ExistUser(arg1);
            return !result;
        }
}