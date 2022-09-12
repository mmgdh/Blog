using BlogInfoService.Domain;
using BlogInfoService.Infrastructure;
using BlogInfoService.WebAPI.ViewModels.RequestModel;
using BlogInfoService.WebAPI.ViewModels.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogInfoService.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BlogParameterController : Controller
    {
        private BlogParameterDomainService blogParameterDomainService;
        private BlogParamDbContext dbContext;

        public BlogParameterController(BlogParameterDomainService blogParameterDomainService, BlogParamDbContext dbContext)
        {
            this.blogParameterDomainService = blogParameterDomainService;
            this.dbContext = dbContext;
        }
        [HttpPost]
        public async Task<bool> AddBlogParameter(AddBlogParameterRequest request)
        {
            var blogParameter = await blogParameterDomainService.CreateBlogParameter(request.ParamName, request.ParamValue, request.ParamType);
            dbContext.Add(blogParameter);
            return await dbContext.SaveChangesAsync() > 0;
        }
        [HttpDelete]
        public async Task<bool> DelBlogParameter(Guid id)
        {
            var Param = await dbContext.blogParameters.FindAsync(id);
            if (Param == null) throw new Exception("查询不到对应参数");
            dbContext.Remove(Param);
            return await dbContext.SaveChangesAsync() > 0;
        }
        [HttpPut]
        public async Task<bool> ModifyBlogParameter(ModifyBlogParameterRequest request)
        {
            var Param = await dbContext.blogParameters.FindAsync(request.id);
            if (Param == null) throw new Exception("查询不到对应参数");
            Param.ParamValue = request.paramValue;
            return await dbContext.SaveChangesAsync() > 0;
        }
        [HttpGet]
        public async Task<List<BlogParamterResponse>> GetAllBlogParameters()
        {
            var blogParamters = await dbContext.blogParameters.ToListAsync();
            return BlogParamterResponse.CreatelstResponse(blogParamters);
        }
        [HttpGet]
        public async Task<List<BlogParamterResponse>> GetBlogParameters([FromBody] List<string> array)
        {
            var blogParamters = await dbContext.blogParameters.Where(x => array.Contains(x.ParamName)).ToListAsync();
            return BlogParamterResponse.CreatelstResponse(blogParamters);
        }
        [HttpGet]
        public async Task<BlogParamterResponse> GetBlogParameter(string ParamName)
        {
            var blogParamter = await dbContext.blogParameters.Where(x => ParamName == x.ParamName).FirstOrDefaultAsync();
            if (blogParamter == null) throw new Exception("未找到对应的参数信息");
            return BlogParamterResponse.CreateResponse(blogParamter);
        }
    }
}
