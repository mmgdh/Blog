using EventBus;
using FileService.Domain;

namespace FileService.WebAPI.EventHandlers
{
    [EventName("FileUpload")]
    public class FileUploadEventHandler : DynamicIntegrationEventHandler
    {
        private readonly IUploadItemRepository repository;

        public FileUploadEventHandler(IUploadItemRepository repository)
        {
            this.repository = repository;
        }

        public override Task HandleDynamic(string eventName, dynamic eventData)
        {
            IFormFile formFile = eventData.FormFile;
            var ret = repository.UploadFileAsync(formFile);
            return ret;
        }
    }
}
