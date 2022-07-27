namespace ArticleService.WebAPI.Controllers.ViewModels.RequestModel
{
    public record ArticleModifyRequest(Guid id,string title,string Content,Guid classify, List<ArticleTagRequest> tags)
    {

    }
}
