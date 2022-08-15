using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StreamService.Domain;
using StreamService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StreamService.Infrastructure.StorageClients
{
    internal class SMMSStorage : IStorageClient
    {
        public EnumStorageType StorageType => EnumStorageType.SMMS;

        public IHttpClientFactory httpClientFactory;

        public SMMSStorage(IHttpClientFactory httpClientFactory)
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

        public async Task<Uri> UploadFileASync(string key, IFormFile file)
        {
            var url = "https://sm.ms/api/v2/upload";
            string str = "";
            //上传图片到服务器
            using (Stream fs = file.OpenReadStream())
            {
                HttpClient client = httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("acMnBVLpF7wZVWiY8eLhxWKY57cU6eus");
                MultipartFormDataContent form = new MultipartFormDataContent();//表单
                StreamContent fileContent = new StreamContent(fs);//图片stream
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                fileContent.Headers.ContentDisposition.FileName = file.FileName;
                fileContent.Headers.ContentDisposition.Name = "smfile";
                form.Add(fileContent);

                form.Add(new StringContent("json"), "format");

                HttpResponseMessage resp = client.PostAsync(url, form).Result;
                var resContent = await resp.Content.ReadAsStringAsync();

                if (resp.IsSuccessStatusCode)
                {
                    var json = JsonConvert.DeserializeObject<ResponseData>(resContent);
                    if (json != null && json.success)
                    {
                        return new Uri(json.data.url);
                    }
                }
                else
                {
                    throw new Exception("上传图片至SMMS失败，错误信息：" + resp.ToString());
                }
                throw new Exception("上传图片至SMMS失败，错误信息：" + resContent);
            }
        }
        private class ResponseData
        {
            public bool success { get; set; }
            public Data data { get; set; }
        }
        private class Data
        {
            public string url { get; set; }
        }
    }
}
