using FluentValidation;

namespace IdentityService.WebAPI.Controllers.ViewModels.RequestModel.Login;
public record LoginByPhoneAndCodeRequest(string PhoneNum, string Code);
public class LoginByPhoneAndCodeRequestValidator : AbstractValidator<LoginByPhoneAndCodeRequest>
{
    public LoginByPhoneAndCodeRequestValidator()
    {
        RuleFor(e => e.PhoneNum).NotNull().NotEmpty();
        RuleFor(e => e.Code).NotNull().NotEmpty();
    }
}