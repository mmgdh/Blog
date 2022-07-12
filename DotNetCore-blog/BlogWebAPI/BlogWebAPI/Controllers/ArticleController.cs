using ArticleService.Domain;
using ArticleService.Domain.Entities;
using ArticleService.Domain.IRepository;
using ArticleService.Infrastructure;
using ArticleService.WebAPI.Controllers.ViewModels;
using EventBus;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository repository;
        private readonly IArticleTagRepository Tagrepository;
        private readonly ArticleDomainService domainService;
        private readonly ArticleDbContext dbCtx;

        private IEventBus eventBus;

        public ArticleController(IArticleRepository _repository, ArticleDomainService _domainService, ArticleDbContext _dbCTx, IArticleTagRepository tagrepository, IEventBus eventBus)
        {
            repository = _repository;
            domainService = _domainService;
            dbCtx = _dbCTx;
            Tagrepository = tagrepository;
            this.eventBus = eventBus;
        }

        #region Article相关API
        [HttpPost]
        public async Task<ActionResult<Guid>> Add(ArticleAddRequest request)
        {
            List<Guid> TagsGuid = request.Tags.Select(x => x.id).ToList();

            //var ArticleTags = request.ToArticleTagArray(request.Tags);
            Article AddArticle = Article.Create(request.Title, request.content);

            //从数据库获取Classify并保存关系
            var Classify = await dbCtx.ArticleClassifies.FindAsync(request.Classify);
            AddArticle.Classify = Classify;
            Classify.Articles.Add(AddArticle);
            //从数据库获取对应的Tags并保存关系
            var ArticleTags = dbCtx.Tags.Where(x => TagsGuid.Contains(x.Id)).ToList();
            AddArticle.Tags = ArticleTags;
            ArticleTags.ForEach(x => x.Articles = new List<Article>() { AddArticle });


            dbCtx.Articles.Add(AddArticle);
            dbCtx.Tags.UpdateRange(ArticleTags);
            await dbCtx.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<Article[]>> GetArticleByPage(int? page, int? pageSize)
        {
            if (page == null)
                page = 0;
            if (pageSize == null)
                pageSize = 10;
            var ret = await repository.GetArticleByPageAsync((int)page, (int)pageSize);
            return ret;
        } 
        #endregion

        #region Tag相关API
        [HttpPost]
        public async Task<ActionResult<Guid>> AddTag(string TagName)
        {
            var Tag = await domainService.CreateTag(TagName);
            dbCtx.Add(Tag);
            await dbCtx.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<ArticleTag[]>> GetAllTags()
        {
            return await Tagrepository.GetAllArticleTagsAsync();
        }
        #endregion

        #region Classify相关API
        [HttpPost]
        public async Task<ActionResult<Guid>> AddClassify(string ClassName, IFormFile formFile)
        {
            eventBus.publish("FileUpload", formFile);
            var Classify = await domainService.CreateClassify(ClassName);
            dbCtx.Add(Classify);
            await dbCtx.SaveChangesAsync();
            return Ok();
        } 
        #endregion



    }
}
