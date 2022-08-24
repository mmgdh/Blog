using COSXML;
using COSXML.Auth;
using COSXML.Model.Object;
using COSXML.Transfer;
using Microsoft.AspNetCore.Http;
using StreamService.Domain;
using StreamService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamService.Infrastructure.StorageClients
{
    internal class TencentCosStorage : IStorageClient
    {
        public EnumStorageType StorageType => throw new NotImplementedException();
        CosXmlConfig config;
        CosXml cosXml;
        QCloudCredentialProvider cosCredentialProvider;

        public TencentCosStorage(CosXmlConfig config, CosXml cosXml, QCloudCredentialProvider cosCredentialProvider)
        {
            this.config = config;
            this.cosXml = cosXml;
            this.cosCredentialProvider = cosCredentialProvider;
        }

        public Task<Tuple<byte[], string>> GetUploadFileByteArray(UploadUri uploadUri)
        {
            throw new NotImplementedException();
        }

        public async Task<Uri> UploadFileASync(string key, IFormFile file)
        {
            try
            {
                // 存储桶名称，此处填入格式必须为 bucketname-APPID, 其中 APPID 获取参考 https://console.cloud.tencent.com/developer
                string bucket = "blog-img-1302438275";
                Stream fileStream = file.OpenReadStream();
                // 组装上传请求，其中 offset sendLength 为可选参数
                long offset = 0L;
                long sendLength = fileStream.Length;
                PutObjectRequest request = new PutObjectRequest(bucket, key, fileStream, offset, sendLength);
                //设置进度回调
                request.SetCosProgressCallback(delegate (long completed, long total)
                {
                    Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                });
                //执行请求
                PutObjectResult result = cosXml.PutObject(request);
                //关闭文件流
                fileStream.Close();
                //对象的 eTag
                string eTag = result.eTag;
                //对象的 crc64ecma 校验值
                string crc64ecma = result.crc64ecma;
                //打印请求结果
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }



            return new Uri("123");
        }
    }
}
