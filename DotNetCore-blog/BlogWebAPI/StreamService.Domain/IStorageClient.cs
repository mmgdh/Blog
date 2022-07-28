using Microsoft.AspNetCore.Http;
using StreamService.Domain.Entities;

namespace StreamService.Domain
{
    public interface IStorageClient
    {
        EnumStorageType StorageType { get; }
        public  Task<Uri> UploadFileASync(string key,IFormFile stream);

        public Task<Tuple<Byte[],string>> GetUploadFileByteArray(UploadUri uploadUri);
    }
}
