using FileService.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Domain
{
    public interface IUploadItemRepository
    {
        public Task<UploadItem?> GetUploadItemAsync(Guid id);

        public Task<Uri> UploadFileAsync(IFormFile formFile);
        
    }
}
