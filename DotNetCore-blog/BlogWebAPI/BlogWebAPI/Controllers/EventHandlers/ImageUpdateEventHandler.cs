using ArticleService.Domain;
using ArticleService.Domain.IRepository;
using ArticleService.Infrastructure;
using EventBus;

namespace ArticleService.WebAPI.Controllers.EventHandlers
{
    [EventName("TitleImageUpload")]
    public class ImageUpdateEventHandler : DynamicIntegrationEventHandler
    {
        IArticleRepository articleRepository;
        ArticleDbContext dbContext;

        public ImageUpdateEventHandler(IArticleRepository articleRepository, ArticleDbContext dbContext)
        {
            this.articleRepository = articleRepository;
            this.dbContext = dbContext;
        }

        public override Task HandleDynamic(string eventName, dynamic eventData)
        {
            Guid guid = eventData.Id;
            var Article = dbContext.Articles.Find(guid);
            return Task.CompletedTask;
        }
    }
}
