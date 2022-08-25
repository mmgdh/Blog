using Microsoft.AspNetCore.Http;
using StreamService.Domain.Entities;

namespace StreamService.Domain
{
    public interface IUploadItemRepository
    {
        public Task<UploadItem?> GetUploadItemAsync(Guid id);

        public Task<UploadItem> UploadFileAsync(string UploadType, IFormFile formFile);

        public Task<Tuple<Byte[],string>> GetFastFile(Guid id);
        
    }
}
