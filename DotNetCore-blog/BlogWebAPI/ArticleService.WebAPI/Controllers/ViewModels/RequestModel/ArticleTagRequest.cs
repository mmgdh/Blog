using ArticleService.Domain;
using ArticleService.Domain.Entities;
using FluentValidation;

namespace ArticleService.WebAPI.Controllers.ViewModels.RequestModel
{
    public record ArticleTagRequest(string TagName, Guid id)
    {
        public ArticleTag ToArticleTag()
        {
            return new ArticleTag
            {
                TagName = TagName,
                Id = id,
                Articles = new List<Article>()
            };
        }
        public class ArticleTagRequestValidator : AbstractValidator<ArticleTagRequest>
        {
            public ArticleTagRequestValidator()
            {
                RuleFor(x => x.id).NotEmpty();
            }
        }

    }

}
