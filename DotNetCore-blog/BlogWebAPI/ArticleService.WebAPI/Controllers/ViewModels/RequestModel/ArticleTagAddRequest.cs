using FluentValidation;

namespace ArticleService.WebAPI.Controllers.ViewModels.RequestModel
{
    public record ArticleTagAddRequest(string tagName)
    {
    }
    public class ArticleTagAddRequestValidator : AbstractValidator<ArticleTagAddRequest>
    {
        public ArticleTagAddRequestValidator()
        {
            RuleFor(x => x.tagName).NotEmpty();
        }
    }
}
