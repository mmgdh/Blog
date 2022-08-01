using FluentValidation;

namespace IdentityService.WebAPI.Controllers.ViewModels.RequestModel.Login;
public record SendCodeByPhoneRequest(string PhoneNumber);
public class SendCodeByPhoneRequestValidator : AbstractValidator<SendCodeByPhoneRequest>
{
    public SendCodeByPhoneRequestValidator()
    {
        RuleFor(e => e.PhoneNumber).NotNull().NotEmpty();
    }
}