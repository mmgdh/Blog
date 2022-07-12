using Commons;
using EventBus;
using FileService.Domain;

namespace FileService.WebAPI.EventHandlers
{
    [EventName(ConstEventName.FileUpload)]
    public class FileUploadEventHandler : DynamicIntegrationEventHandler
    {
        private readonly IUploadItemRepository repository;


        public FileUploadEventHandler(IUploadItemRepository repository)
        {
            this.repository = repository;
        }

        public override Task HandleDynamic(string eventName, dynamic eventData)
        {
            var bytes = ImageHelper.Base64ToImage(eventData.Base64);
            using Stream stream = new MemoryStream(bytes);
            FormFile formFile = new FormFile(stream, eventData.Offset,eventData.Length, eventData.Name, eventData.FileName);
            //FormFile a = new FormFile();
            //IFormFile formFile = ;
            var ret = repository.UploadFileAsync(formFile);
            return ret;
        }
    }
}
