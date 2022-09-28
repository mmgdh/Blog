using ArticleService.Domain;
using ArticleService.Infrastructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using CommonInfrastructure;
using ArticleService.Domain.Entities;

namespace ArticleService.WebAPI.Controllers.ViewModels.RequestModel
{
    public record ArticleAddRequest(string Title, string content,string html, Guid Classify, Guid[] Tags, IFormFile? file)
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
            RuleFor(x => x.html).NotEmpty();
            RuleFor(x => x.Tags).NotEmpty();
            //异步报错了，后续再解决
            RuleFor(x => x.Tags).Must((Tags) => dbCtx.Query<ArticleTag>().Any(T => Tags.Contains(T.Id)))
                .WithMessage(c => $"所选标签在数据库内不存在，请重新选择");
        }
    }
}
