using FluentValidation;

namespace ArticleService.WebAPI.Controllers.ViewModels.RequestModel
{
    public record ArticleClassifyRequest(Guid Id, string ClassifyName, IFormFile? file)
    {
    }

    public class ArticleClassifyValidator : AbstractValidator<ArticleClassifyRequest>
    {
        public ArticleClassifyValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    public record ArticleClassifyAddRequest(string ClassifyName, IFormFile file) { }

    public class ArticleClassifyAddValidator : AbstractValidator<ArticleClassifyAddRequest>
    {
        public ArticleClassifyAddValidator()
        {
            RuleFor(x => x.ClassifyName).NotEmpty();
        }
    }
}
