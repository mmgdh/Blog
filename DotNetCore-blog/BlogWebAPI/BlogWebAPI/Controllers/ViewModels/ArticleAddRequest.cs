using ArticleService.Domain;

namespace ArticleService.WebAPI.Controllers.ViewModels
{
    public record ArticleAddRequest(string? Title,string? content, ArticleTagR[]? Tags)
    {
    }

    public record ArticleTagR(string TagName, string id) { }
}
