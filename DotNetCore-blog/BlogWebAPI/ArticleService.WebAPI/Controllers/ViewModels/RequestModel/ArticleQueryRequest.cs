namespace ArticleService.WebAPI.Controllers.ViewModels.RequestModel
{
    public record ArticleQueryRequest(int page,int pageSize,DateTime? Date, string[]? ClassifyIds)
    {
    }
}
