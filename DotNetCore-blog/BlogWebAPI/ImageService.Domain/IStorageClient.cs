using FileService.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Domain
{
    public interface IStorageClient
    {
        EnumStorageType StorageType { get; }
        public  Task<Uri> UploadFileASync(string key,IFormFile stream);

        public Task<Uri> GetUploadUri(UploadUri uploadUri);
    }
}
