namespace ArticleService.WebAPI.Controllers.ViewModels.RequestModel
{
    public record ArticleModifyRequest(Guid Id,string Title,string Content,Guid classify, List<ArticleTagRequest> Tags)
    {

    }
}
