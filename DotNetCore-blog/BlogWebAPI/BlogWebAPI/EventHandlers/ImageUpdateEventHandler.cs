using ArticleService.Domain;
using ArticleService.Domain.IRepository;
using ArticleService.Infrastructure;
using EventBus;

namespace ArticleService.WebAPI.EventHandlers
{
    [EventName(ConstEventName.Article_FileCallBackUpdated)]
    public class ImageUpdateEventHandler : JsonIntegrationEventHandler<EventBusParameter.CallBackUpdateEntity>
    {
        IArticleRepository articleRepository;
        ArticleDbContext dbContext;

        public ImageUpdateEventHandler(IArticleRepository articleRepository, ArticleDbContext dbContext)
        {
            this.articleRepository = articleRepository;
            this.dbContext = dbContext;
        }

        public override async Task HandleJson(string eventName, EventBusParameter.CallBackUpdateEntity? eventData)
        {
            if (eventData == null) return;
            var ret = eventData.CallBackNeed;
            if (ret == null) return;
            switch (ret.CallBackEntity)
            {
                case EnumCallBackEntity.ArticleClassify:
                    var ArticleClassify = await dbContext.ArticleClassifies.FindAsync(ret.MasterGuidId);
                    if (ArticleClassify == null) throw new Exception("未找到对应分类");
                    ArticleClassify.DefaultImgId = eventData.FileGuidId;
                    break;
                case EnumCallBackEntity.ArticleTitleImage:
                    var Article = await dbContext.Articles.FindAsync(ret.MasterGuidId);
                    if (Article == null) throw new Exception("未找到对应文章");
                    Article.ImageId = eventData.FileGuidId;
                    break;
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
