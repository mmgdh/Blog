﻿using ArticleService.Domain;
using ArticleService.Domain.Entities;
using ArticleService.Domain.IRepository;
using ArticleService.Infrastructure;
using ArticleService.WebAPI.Controllers.ViewModels.RequestModel;
using ArticleService.WebAPI.Controllers.ViewModels.ResponseModel;
using CommomConst;
using CommonHelpers;
using CommonInfrastructure;
using Commons;
using EventBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository repository;
        private readonly IArticleTagRepository Tagrepository;
        private readonly IArticleClassifyRepository classifyRepository;
        private readonly ArticleDomainService domainService;
        private readonly ArticleDbContext dbCtx;

        private RedisHelper redisHelper;

        private IEventBus eventBus;

        public ArticleController(IArticleRepository _repository, ArticleDomainService _domainService, ArticleDbContext _dbCTx, IArticleTagRepository tagrepository, IEventBus eventBus, IArticleClassifyRepository classifyRepository, RedisHelper redisHelper)
        {
            repository = _repository;
            domainService = _domainService;
            dbCtx = _dbCTx;
            Tagrepository = tagrepository;
            this.eventBus = eventBus;
            this.classifyRepository = classifyRepository;
            this.redisHelper = redisHelper;
        }

        #region Article相关API
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Guid>> Add(ArticleAddRequest request)
        {
            List<Guid> TagsGuid = new List<Guid>();
            if (request.Tags != null)
                TagsGuid = request.Tags.Select(x => x.id).ToList();
            Article AddArticle = Article.Create(request.Title, request.content);

            //从数据库获取Classify并保存关系
            var Classify = await dbCtx.ArticleClassifies.FindAsync(request.Classify);
            if (Classify == null) throw new Exception("更新文章失败，未获取对应的分类");
            AddArticle.Classify = Classify;
            Classify.Articles.Add(AddArticle);
            //从数据库获取对应的Tags并保存关系
            var ArticleTags = dbCtx.Tags.Where(x => TagsGuid.Contains(x.Id)).ToList();
            AddArticle.Tags = ArticleTags;
            ArticleTags.ForEach(x => x.Articles.Add(AddArticle));

            dbCtx.Articles.Add(AddArticle);
            dbCtx.Tags.UpdateRange(ArticleTags);
            await dbCtx.SaveChangesAsync();


            return AddArticle.Id;
        }
        [HttpPut]
        [Authorize]
        public async Task<Article> Modify(ArticleModifyRequest request)
        {

            List<Guid> TagsGuid = request.tags.Select(x => x.id).ToList();
            Article? ModifyArticle = await dbCtx.Articles.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == request.id);
            if (ModifyArticle == null) throw new Exception("更新文章失败，通过Id未找到对应的文章");
            ModifyArticle.Title = request.title;
            ModifyArticle.articleContent = ArticleContent.Create(ModifyArticle, request.Content);

            //从数据库获取Classify并保存关系
            var Classify = await dbCtx.ArticleClassifies.FindAsync(request.classify);
            if (Classify == null) throw new Exception("更新文章失败，未获取对应的分类");
            ModifyArticle.Classify = Classify;
            //从数据库获取对应的Tags并保存关系
            var ArticleTags = dbCtx.Tags.Include(x => x.Articles).Where(x => TagsGuid.Contains(x.Id)).ToList();
            ModifyArticle.Tags = ArticleTags;

            await dbCtx.SaveChangesAsync();

            return ModifyArticle;
        }

        [HttpDelete]
        [Authorize]
        public async Task<bool> Delete(Guid id)
        {
            Article? DeleteArticle = await dbCtx.Articles.FindAsync(id);
            if (DeleteArticle == null)
                return false;
            dbCtx.Remove(DeleteArticle);
            await dbCtx.SaveChangesAsync();
            //if (DeleteArticle == null) throw new Exception("文章已被删除");
            return true;
        }

        [HttpGet]
        public async Task<ActionResult<lstArticleResp?>> GetArticleByPage([FromQuery] ArticleQueryRequest request)
        {

            var query = dbCtx.Articles.Include(x => x.Tags).Include(x => x.Classify).Where(x => 1 == 1);
            if (request.ClassifyIds != null && request.ClassifyIds.Count() > 0)
            {
                query = query.Where(x => request.ClassifyIds.Any(y => y == x.Classify.Id.ToString()));
            }
            var ArticleCount = query.Count();
            var ret = await query.OrderByDescending(x => x.CreationTime).Skip((request.page - 1) * request.pageSize).Take(request.pageSize).ToArrayAsync();
            lstArticleResp response = new lstArticleResp();

            response.articles = lstArticleResp.CreateArticles(ret);
            response.PageArticleCount = ArticleCount;
            return response;
        }
        [HttpGet]
        public async Task<ArticleResp> GetArticleById(Guid id)
        {
            var ret = await repository.GetArticleByIdAsync(id);
            if (ret == null)
            {
                throw new Exception("未找到对应的文章");
            }
            return ArticleResp.Create(ret);
        }
        [HttpGet]
        public async Task<int> GetArticleCount()
        {
            return await dbCtx.Articles.CountAsync();
        }
        #endregion

        #region Tag相关API
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Guid>> AddTag(ArticleTagAddRequest AddRequest)
        {
            var Tag = await domainService.CreateTag(AddRequest.tagName);
            dbCtx.Add(Tag);
            await dbCtx.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ArticleTag>> ModifyTag(ArticleTagRequest articleTag)
        {
            var Tag = await dbCtx.Tags.FindAsync(articleTag.id);
            Tag.TagName = articleTag.TagName;
            await dbCtx.SaveChangesAsync();
            return Tag;
        }
        [HttpDelete]
        [Authorize]
        public async Task<bool> DeleteTag(Guid id)
        {
            var ret = await Tagrepository.TagDeletAsync(id);
            if (ret)
                await dbCtx.SaveChangesAsync();
            return ret;
        }
        [HttpGet]
        public async Task<ActionResult<ArticleTag[]>> GetAllTags()
        {
            return await Tagrepository.GetAllArticleTagsAsync();
        }
        #endregion

        #region Classify相关API
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Guid>> AddClassify([FromForm] ArticleClassifyAddRequest articleClassifyAdd)
        {
            var Classify = await domainService.CreateClassify(articleClassifyAdd.ClassifyName);
            dbCtx.Add(Classify);
            EventBusHelper.EventBusFunc_UploadImg(UploadImageType.ArticleClassifyImage, articleClassifyAdd.file, Classify.Id, eventBus);
            await dbCtx.SaveChangesAsync();
            return Classify.Id;
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<bool>> ModifyCLassify([FromForm]ArticleClassifyRequest articleClassify)
        {
            var Classify = await dbCtx.ArticleClassifies.FindAsync(articleClassify.Id);
            if (Classify == null) throw new Exception("未找到对应的文章分类");
            Classify.ClassifyName = articleClassify.ClassifyName;
            if (articleClassify.file != null)
                EventBusHelper.EventBusFunc_UploadImg(UploadImageType.ArticleClassifyImage, articleClassify.file, Classify.Id, eventBus);
            await dbCtx.SaveChangesAsync();
            return true;
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteClassify(Guid id)
        {
            var ret = await classifyRepository.ArticleClassifyDelete(id);
            if (ret)
                await dbCtx.SaveChangesAsync();
            return ret;
        }
        [HttpGet]
        public async Task<ActionResult<ArticleClassifyResponse[]>> GetAllArticleClassify()
        {
            var classifys = await classifyRepository.GetAllArticleClassifyAsync();
            var ret = classifys.Select(x => new ArticleClassifyResponse(x));
            return Ok(ret);
        }
        #endregion



    }
}
