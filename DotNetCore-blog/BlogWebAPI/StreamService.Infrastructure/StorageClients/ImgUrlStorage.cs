using Microsoft.AspNetCore.Http;
using StreamService.Domain;
using StreamService.Domain.Entities;
using System.Net;
using System.Net.Http.Json;

namespace StreamService.Infrastructure.StorageClients
{
    internal class ImgUrlStorage : IStorageClient
    {
        public EnumStorageType StorageType => EnumStorageType.UrlImg;
        private IHttpClientFactory httpClientFactory;

        public ImgUrlStorage(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<Tuple<byte[], string>> GetUploadFileByteArray(UploadUri uploadUri)
        {
            HttpClient client = httpClientFactory.CreateClient();
            var resp = await client.GetAsync(uploadUri.Uri);
            try
            {
                if (resp.IsSuccessStatusCode)
                {
                    var bytes = await resp.Content.ReadAsByteArrayAsync();
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

        public async Task<Uri> UploadFileASync(string key, IFormFile stream)
        {
            var Url = "https://www.imgurl.org/api/v2/upload";
            var cl = httpClientFactory.CreateClient();
            HttpResponseMessage httpResponse = await cl.PostAsJsonAsync(Url, new
            {
                file = stream,
                uid = "e9e3f2673be6f9be08198525b1bb1176",
                token = "c6fd40e50626ae7cc5682052ad679a48"
            });
            string result = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                //return httpResponse.Content.
            }
            throw new Exception("出错");
            return null;
        }
    }
}
