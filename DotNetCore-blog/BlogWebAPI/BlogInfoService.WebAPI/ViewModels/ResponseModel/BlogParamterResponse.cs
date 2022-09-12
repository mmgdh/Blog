using BlogInfoService.Domain.Entities;

namespace BlogInfoService.WebAPI.ViewModels.ResponseModel
{
    public record BlogParamterResponse(Guid id,string paramName, string paramValue)
    {
        public static BlogParamterResponse CreateResponse(BlogParameter blogParameter)
        {
            BlogParamterResponse blogParamterResp = new BlogParamterResponse(blogParameter.Id,blogParameter.ParamName, blogParameter.ParamValue);
            return blogParamterResp;
        }
        public static List<BlogParamterResponse> CreatelstResponse(IEnumerable<BlogParameter> lstBlogParameter)
        {
            List<BlogParamterResponse> paramterResponses = new List<BlogParamterResponse>();
            foreach (var param in lstBlogParameter)
            {
                paramterResponses.Add(CreateResponse(param));
            }
            return paramterResponses;
        }
    }
}
