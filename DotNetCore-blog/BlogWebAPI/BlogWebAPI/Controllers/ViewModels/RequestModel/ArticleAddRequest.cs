using ArticleService.Domain;
using ArticleService.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using CommonInfrastructure;
using ArticleService.Domain.Entities;

namespace ArticleService.WebAPI.Controllers.ViewModels.RequestModel
{
    public record ArticleAddRequest(string Title, string content, Guid Classify, Guid? ImgId, ArticleTagRequest[]? Tags)
    {
        public ArticleTag[] ToArticleTagArray(ArticleTagRequest[] tagRequests)
        {
            return tagRequests.Select(x => x.ToArticleTag()).ToArray();
        }
    }

    public class ArticleAddRequestValidator : AbstractValidator<ArticleAddRequest>
    {
        public ArticleAddRequestValidator(ArticleDbContext dbCtx)
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.content).NotEmpty();
            RuleFor(x => x.Tags).NotEmpty();

            RuleFor(x => x.Tags).MustAsync((Tags, cancle) => dbCtx.Query<ArticleTag>().AnyAsync(T => Tags.Select(x => x.id).Contains(T.Id)))
                .WithMessage(c => $"所选标签在数据库内不存在，请重新选择");
        }
    }
}
