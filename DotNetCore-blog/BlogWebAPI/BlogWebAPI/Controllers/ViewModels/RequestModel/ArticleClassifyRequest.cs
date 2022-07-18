using FluentValidation;

namespace ArticleService.WebAPI.Controllers.ViewModels.RequestModel
{
    public record ArticleClassifyRequest(string ClassifyName, IFormFile Img)
    {
    }

    public class ArticleClassifyValidator : AbstractValidator<ArticleClassifyRequest>
    {

    }
}
