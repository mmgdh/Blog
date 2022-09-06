using BlogInfoService.Domain;
using BlogInfoService.Infrastructure;
using BlogInfoService.WebAPI.ViewModels.RequestModel;
using BlogInfoService.WebAPI.ViewModels.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogInfoService.WebAPI.Controllers
{
    public class BlogParameterController : Controller
    {
        private BlogParameterDomainService blogParameterDomainService;
        private BlogParamDbContext dbContext;

        public BlogParameterController(BlogParameterDomainService blogParameterDomainService)
        {
            this.blogParameterDomainService = blogParameterDomainService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<bool> AddBlogParamter(AddBlogParameterRequest request)
        {
            var blogParameter = await blogParameterDomainService.CreateBlogParameter(request.BlogParamName, request.BlogParamValue);
            dbContext.Add(blogParameter);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<BlogParamterResponse>> GetBlogParamters(List<string> array)
        {
          var blogParamters = await dbContext.blogParameters.Where(x => array.Contains(x.ParamName)).ToListAsync();
            return BlogParamterResponse.CreatelstResponse(blogParamters);
        }
    }
}
