namespace ArticleService.WebAPI.Controllers.ViewModels.RequestModel
{
    public record ArticleModifyRequest(Guid id,string title,string Content,string Html,Guid classify, Guid[] tags,IFormFile? file)
    {

    }
}
