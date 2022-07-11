using FluentValidation;

namespace ArticleService.WebAPI.Controllers.ViewModels
{
    public record ArticleClassifyRequest(string ClassifyName,IFormFile Img)
    {
    }

    public class ArticleClassifyValidator : AbstractValidator<ArticleClassifyRequest>
    {

    }
}
