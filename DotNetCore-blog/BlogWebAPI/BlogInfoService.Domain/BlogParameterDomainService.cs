using BlogInfoService.Domain.Entities;
using BlogInfoService.Domain.Entities.IRepository;

namespace BlogInfoService.Domain
{
    public class BlogParameterDomainService
    {
        IBlogParamRepository blogParamRepository;
        public BlogParameterDomainService(IBlogParamRepository blogParamRepository)
        {
            this.blogParamRepository = blogParamRepository;
        }

        public async Task<BlogParameter> CreateBlogParameter(string ParamName, string ParamValue, string? ParamType)
        {
            if (await blogParamRepository.JudgeBlogParamIsExistAsync(ParamName))
            {
                throw new Exception($"参数【{ParamName}】已存在，无法新增！");
            }
            return BlogParameter.Create(ParamName, ParamValue, ParamType);
        }
    }
}
