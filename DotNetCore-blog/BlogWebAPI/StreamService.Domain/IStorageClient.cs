using Microsoft.AspNetCore.Http;
using StreamService.Domain.Entities;

namespace StreamService.Domain
{
    public interface IStorageClient
    {
        EnumStorageType StorageType { get; }
        public  Task<Tuple<string,string>> UploadFileASync(string key,IFormFile file);

        public Task<Tuple<Byte[],string>> GetUploadFileByteArray(UploadUri uploadUri);
    }
}
