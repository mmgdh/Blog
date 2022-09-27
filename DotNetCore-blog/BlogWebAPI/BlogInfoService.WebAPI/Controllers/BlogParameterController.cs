using BlogInfoService.Domain;
using BlogInfoService.Domain.Entities;
using BlogInfoService.Infrastructure;
using BlogInfoService.WebAPI.ViewModels.RequestModel;
using BlogInfoService.WebAPI.ViewModels.ResponseModel;
using CommonHelpers;
using Microsoft.AspNetCore.Authorization;
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
        private readonly RedisHelper redisHelper;
        const string KeyName = "Blog-BlogInfo-AllParameters";

        public BlogParameterController(BlogParameterDomainService blogParameterDomainService, BlogParamDbContext dbContext, RedisHelper redisHelper)
        {
            this.blogParameterDomainService = blogParameterDomainService;
            this.dbContext = dbContext;
            this.redisHelper = redisHelper;
            redisHelper.DbNum = 3;
        }
        [HttpPost]
        [Authorize]
        public async Task<bool> AddBlogParameter(AddBlogParameterRequest request)
        {
            var blogParameter = await blogParameterDomainService.CreateBlogParameter(request.ParamName, request.ParamValue, request.ParamType);
            dbContext.Add(blogParameter);
            if (redisHelper.KeyExists(KeyName))
            {
                await redisHelper.ListPushOneAsync(KeyName, blogParameter);
            }
            return await dbContext.SaveChangesAsync() > 0;
        }
        [HttpDelete]
        [Authorize]
        public async Task<bool> DelBlogParameter(Guid id)
        {
            var Param = await dbContext.blogParameters.FindAsync(id);
            if (Param == null) throw new Exception("查询不到对应参数");
            dbContext.Remove(Param);
            var ret =await dbContext.SaveChangesAsync() > 0;
            if (ret)
            {
                await redisHelper.ReSetRedisValue(KeyName, reSetFunc: new Func<Task<List<BlogParameter>>>(async () => { return await dbContext.blogParameters.ToListAsync(); }));
            }
            return ret;
        }
        [HttpPut]
        [Authorize]
        public async Task<bool> ModifyBlogParameter(ModifyBlogParameterRequest request)
        {
            var Param = await dbContext.blogParameters.FindAsync(request.id);
            if (Param == null) throw new Exception("查询不到对应参数");
            Param.ParamValue = request.paramValue;
            var ret = await dbContext.SaveChangesAsync() > 0;
            if (ret)
            {
                await redisHelper.ReSetRedisValue(KeyName, reSetFunc: new Func<Task<List<BlogParameter>>>(async () => { return await dbContext.blogParameters.ToListAsync(); }));
            }
            return ret;
        }
        [HttpPost]
        [Authorize]
        public async Task<bool> RefreshBlogParameter()
        {
            await redisHelper.ReSetRedisValue(KeyName, reSetFunc: new Func<Task<List<BlogParameter>>>(async () => { return await dbContext.blogParameters.ToListAsync(); }));
            return true;
        }
        [HttpGet]
        public async Task<List<BlogParamterResponse>> GetAllBlogParameters()
        {

            List<BlogParameter> blogParamters = new List<BlogParameter>();
            var redisRet = redisHelper.KeyExists(KeyName);
            if (redisRet)
            {
                blogParamters = await redisHelper.ListRangeAsync<BlogParameter>(KeyName);
            }
            else
            {
                blogParamters = await dbContext.blogParameters.ToListAsync();
                await redisHelper.ReSetRedisValue(KeyName, blogParamters, new Func<Task<List<BlogParameter>>>(async ()=> { return await dbContext.blogParameters.ToListAsync(); } ));
            }


            return BlogParamterResponse.CreatelstResponse(blogParamters);
        }

        [HttpGet]
        public async Task<BlogParamterResponse> GetBlogParameter(string ParamName)
        {
            var blogParamter = await dbContext.blogParameters.Where(x => ParamName == x.ParamName).FirstOrDefaultAsync();
            if (blogParamter == null) throw new Exception("未找到对应的参数信息");
            return BlogParamterResponse.CreateResponse(blogParamter);
        }

        private async Task ReSetBlogParamRedis1(List<BlogParameter>? blogParamters=null)
        {
            //删除后重新设置redis缓存
            redisHelper.KeyDelete(KeyName);
            if (blogParamters==null)
            {
                blogParamters = await dbContext.blogParameters.ToListAsync();
            }
            await redisHelper.AddListAsync<BlogParameter>(KeyName, blogParamters);
            await redisHelper.KeyExpire(KeyName, TimeSpan.FromSeconds(30));
        }
    }
}
