namespace BlogInfoService.WebAPI.ViewModels.RequestModel
{
    public record ModifyBlogParameterRequest(Guid id ,string paramName, string paramValue)
    {
    }
}
