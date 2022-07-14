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
            var ret = eventData.CallBackNeed;
            if (ret == null) return;
            switch (ret.CallBackEntity)
            {
                case EnumCallBackEntity.ArticleClassify:
                    var ArticleClassify = await dbContext.ArticleClassifies.FindAsync(ret.MasterGuidId);
                    ArticleClassify.DefaultImgId = eventData.FileGuidId;
                    break;
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
