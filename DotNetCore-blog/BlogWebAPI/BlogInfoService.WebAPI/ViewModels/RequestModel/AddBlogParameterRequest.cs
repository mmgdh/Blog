using FluentValidation;

namespace BlogInfoService.WebAPI.ViewModels.RequestModel
{
    public record AddBlogParameterRequest(string BlogParamName,string BlogParamValue)
    {
    }

    public class AddBlogParameterRequestValidator : AbstractValidator<AddBlogParameterRequest>
    {
        public AddBlogParameterRequestValidator()
        {
            RuleFor(x => x.BlogParamName).NotEmpty();
        }
    }
}
