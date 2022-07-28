using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using StreamService.Domain;
using StreamService.Domain.Entities;

namespace StreamService.Infrastructure.StorageClients
{
    internal class LocalStorage : IStorageClient
    {
        public EnumStorageType StorageType => EnumStorageType.Local;

        private readonly IHttpClientFactory httpClientFactory;
        private readonly IWebHostEnvironment hostEnv;
        //private readonly IHttpContextAccessor httpContextAccessor;

        private IServerAddressesFeature Iserver { get; set; }

        public LocalStorage(IWebHostEnvironment webHost, IServerAddressesFeature iserver, IHttpClientFactory httpClientFactory)
        {
            hostEnv = webHost;
            Iserver = iserver;
            this.httpClientFactory = httpClientFactory;
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
            if (!string.IsNullOrEmpty(fullDir) &&!Directory.Exists(fullDir))//automatically create dir
            {
                Directory.CreateDirectory(fullDir);
            }
            if (File.Exists(fullPath))//如果已经存在，则尝试删除
            {
                File.Delete(fullPath);
            }
            using FileStream outStream = new FileStream(fullPath, FileMode.Create);
            await formFile.CopyToAsync(outStream);
            //var req = httpContextAccessor.HttpContext.Request;
            //string url = req.Scheme + "://" + req.Host + "/" + key;
            string url = Iserver.Addresses.First() + "/" + key;
            return new Uri(url);
        }

        public async Task<Tuple<byte[],string>> GetUploadFileByteArray(UploadUri uploadUri)
        {
            HttpClient client = httpClientFactory.CreateClient();
            var resp = await client.GetAsync(uploadUri.Uri);
            try
            {
                if (resp.IsSuccessStatusCode)
                {
                    var bytes =await resp.Content.ReadAsByteArrayAsync();
                    if (resp.Content.Headers.ContentType == null) throw new Exception("获取资源失败");
                    return new Tuple<byte[], string>(bytes, resp.Content.Headers.ContentType.ToString());
                }
                else
                {
                   await Task.Delay(3000);
                }

            }
            catch
            {

            }
            throw new Exception("获取资源失败");
        }
    }
}
