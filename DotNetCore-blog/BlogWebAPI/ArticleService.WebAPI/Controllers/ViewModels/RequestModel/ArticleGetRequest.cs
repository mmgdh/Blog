namespace ArticleService.WebAPI.Controllers.ViewModels.RequestModel
{
    public record ArticleGetRequest(Guid[] ids,bool NeedDetail);
}
