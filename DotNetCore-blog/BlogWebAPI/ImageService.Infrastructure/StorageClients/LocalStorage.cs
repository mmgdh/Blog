using FileService.Domain;
using FileService.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.StorageClients
{
    internal class LocalStorage : IStorageClient
    {
        public EnumStorageType StorageType => EnumStorageType.Local;

        private readonly IWebHostEnvironment hostEnv;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalStorage(IWebHostEnvironment webHost, IHttpContextAccessor httpContext)
        {
            hostEnv = webHost;
            httpContextAccessor = httpContext;
        }

        public async Task<Uri> UploadFileASync(string key, IFormFile formFile)
        {
            if (key.StartsWith('/'))
            {
                throw new ArgumentException("key should not start with /", nameof(key));
            }
            string workingDir = Path.Combine(hostEnv.ContentRootPath, "wwwroot");
            string fullPath = Path.Combine(workingDir, key);
            string? fullDir = Path.GetDirectoryName(fullPath);//get the directory
            if (!Directory.Exists(fullDir))//automatically create dir
            {
                Directory.CreateDirectory(fullDir);
            }
            if (File.Exists(fullPath))//如果已经存在，则尝试删除
            {
                File.Delete(fullPath);
            }
            using FileStream outStream = new FileStream(fullPath, FileMode.Create);
            await formFile.CopyToAsync(outStream);
            var req = httpContextAccessor.HttpContext.Request;
            string url = req.Scheme + "://" + req.Host + "/" + key;
            return new Uri(url);
        }
    }
}
