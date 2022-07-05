using ArticleService.Domain;
using ArticleService.Infrastructure;
using ArticleService.WebAPI.Controllers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository repository;
        private readonly ArticleDomainService domainService;
        private readonly ArticleDbContext dbCtx;

        public ArticleController(IArticleRepository _repository,ArticleDomainService _domainService, ArticleDbContext _dbCTx)
        {
            repository = _repository;
            domainService = _domainService;
            dbCtx = _dbCTx;
        }
        [HttpGet]
        public async Task<ActionResult<string>> Index()
        {
            return Ok("11112222");
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add(ArticleAddRequest request)
        {
            List<Guid> TagsGuid = request.Tags.Select(x => x.id).ToList();

            //var ArticleTags = request.ToArticleTagArray(request.Tags);
            Article AddArticle = Article.Create(request.Title, request.content);
            var ArticleTags = dbCtx.Tags.Where(x => TagsGuid.Contains(x.Id)).ToList();
            AddArticle.Tags = ArticleTags;
            ArticleTags.ForEach(x => x.Articles = new List<Article>() { AddArticle });
            dbCtx.Articles.Add(AddArticle);
            dbCtx.Tags.UpdateRange(ArticleTags);
            await dbCtx.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddTag(string TagName)
        {
            var Tag =await domainService.CreateTag(TagName);
            dbCtx.Add(Tag);
            await dbCtx.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<ArticleTag[]>> GetAllTags()
        {
            return await repository.GetAllArticleTagsAsync();
        }
        [HttpGet]
        public async Task<ActionResult<Article[]>> GetArticleByPage(int? page,int? pageSize)
        {
            if (page == null)
                page = 0;
            if (pageSize == null)
                pageSize = 10;
            var ret = await repository.GetArticleByPageAsync((int)page, (int)pageSize);
            return ret;
        }
    }
}
