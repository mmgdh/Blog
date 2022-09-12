using FluentValidation;

namespace BlogInfoService.WebAPI.ViewModels.RequestModel
{
    public record AddBlogParameterRequest(string ParamName,string ParamValue,string? ParamType)
    {
    }

    public class AddBlogParameterRequestValidator : AbstractValidator<AddBlogParameterRequest>
    {
        public AddBlogParameterRequestValidator()
        {
            RuleFor(x => x.ParamName).NotEmpty();
        }
    }
}
