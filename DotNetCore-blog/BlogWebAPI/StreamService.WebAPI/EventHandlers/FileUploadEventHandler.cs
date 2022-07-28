using Commons;
using EventBus;
using StreamService.Domain;
using StreamService.Infrastructure;

namespace StreamService.WebAPI.EventHandlers
{
    [EventName(ConstEventName.FileUpload)]
    public class FileUploadEventHandler : JsonIntegrationEventHandler<EventBusParameter.FileUpload_Parameter>
    {
        private readonly IUploadItemRepository repository;
        private IEventBus eventBus;
        private UploadDbContext dbContext;


        public FileUploadEventHandler(IUploadItemRepository repository, IEventBus eventBus, UploadDbContext dbContext)
        {
            this.repository = repository;
            this.eventBus = eventBus;
            this.dbContext = dbContext;
        }

        //public override Task HandleDynamic(string eventName, dynamic eventData)
        //{
        //    var bytes = ImageHelper.Base64ToImage(eventData.Base64);
        //    using Stream stream = new MemoryStream(bytes);
        //    FormFile formFile = new FormFile(stream, eventData.Offset,eventData.Length, eventData.Name, eventData.FileName);
        //    var ret = repository.UploadFileAsync(formFile);

        //    eventBus.publish(ConstEventName.FileCallBackUpdated, ret.Id);
        //    return ret;
        //}

        public override async Task HandleJson(string eventName, EventBusParameter.FileUpload_Parameter? eventData)
        {
            if (eventData == null) return;
            var FileMessage = eventData.UploadFile;
            var CallBAckMessage = eventData.CallBackNeed;
            var bytes = ImageHelper.Base64ToImage(FileMessage.Base64);
            using Stream stream = new MemoryStream(bytes);
            FormFile formFile = new FormFile(stream, FileMessage.Offset, FileMessage.Length, FileMessage.Name, FileMessage.FileName);
            var ret = await repository.UploadFileAsync(formFile);

            var CallBackEntity = new EventBusParameter.CallBackUpdateEntity(CallBAckMessage, ret.Id);
            await dbContext.SaveChangesAsync();
            eventBus.publish(CallBAckMessage.CallBackEventName, CallBackEntity);

        }
    }
}
