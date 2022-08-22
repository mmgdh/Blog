using FluentValidation;

namespace ArticleService.WebAPI.Controllers.ViewModels.RequestModel
{
    public record ArticleClassifyRequest(Guid Id, string ClassifyName, IFormFile Img)
    {
    }

    public class ArticleClassifyValidator : AbstractValidator<ArticleClassifyRequest>
    {
        public ArticleClassifyValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
