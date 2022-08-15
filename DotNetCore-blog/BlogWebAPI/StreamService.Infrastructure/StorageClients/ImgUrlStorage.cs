﻿using Microsoft.AspNetCore.Http;
using StreamService.Domain;
using StreamService.Domain.Entities;
using System.Net;
using System.Net.Http.Headers;
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

        public async Task<Uri> UploadFileASync(string key, IFormFile file)
        {
            var url = "https://www.imgurl.org/api/v2/upload";
            string str = "";
            //上传图片到服务器
            using (Stream fs = file.OpenReadStream())
            {
                HttpClient client = new HttpClient();
                MultipartFormDataContent form = new MultipartFormDataContent();//表单
                StreamContent fileContent = new StreamContent(fs);//图片stream
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                fileContent.Headers.ContentDisposition.FileName = file.FileName;
                fileContent.Headers.ContentDisposition.Name = "file";
                form.Add(fileContent);

                form.Add(new StringContent("e9e3f2673be6f9be08198525b1bb1176"), "uid");
                form.Add(new StringContent("3e65eb5281910b46104e813b3cc529e8"), "token");

                HttpResponseMessage res = client.PostAsync(url, form).Result;
                str = res.Content.ReadAsStringAsync().Result;

                return new Uri(str);
            }
        }
    }
}