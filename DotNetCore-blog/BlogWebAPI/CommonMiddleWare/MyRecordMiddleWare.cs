using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonMiddleWare
{
    public class MyRecordMiddleWare
    {
        public readonly RequestDelegate _next;

        public readonly ILogger<MyRecordMiddleWare> logger;

        public MyRecordMiddleWare(RequestDelegate next, ILogger<MyRecordMiddleWare> logger)
        {
            _next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                DateTime StartTime = DateTime.Now;
                var request = context.Request;

                var requestIP = GetRemoteIp(context);

                RequestInParam info = new RequestInParam
                {
                    remoteIp = requestIP,
                    path = request.Path,
                    requesttype = request.Method,
                    scheme = request.Scheme,
                    createTime = DateTime.Now,
                };
                // 获取请求body内容
                if (request.Method.ToLower().Equals("post") || request.Method.ToLower().Equals("put"))
                {
                    //启用读取request
                    request.EnableBuffering();

                    //设置当前流中的位置为起点
                    request.Body.Seek(0, SeekOrigin.Begin);

                    //把请求body流转换成字符串
                    info.requestdata = await new StreamReader(request.Body).ReadToEndAsync();

                    request.Body.Seek(0, SeekOrigin.Begin);
                }
                else if (request.Method.ToLower().Equals("get") || request.Method.ToLower().Equals("delete"))
                {
                    info.requestdata = request.QueryString.Value;
                }

                var originalBodyStream = context.Response.Body;
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;
                    await _next(context);

                    //返回结果                        
                    var response = context.Response;

                    info.actiontime = (DateTime.Now - StartTime).TotalSeconds;//执行时间差
                                                                              //设置当前流中的位置为起点
                    responseBody.Seek(0, SeekOrigin.Begin);

                    var responesInfo = await new StreamReader(responseBody).ReadToEndAsync();

                    //设置当前流中的位置为起点
                    responseBody.Seek(0, SeekOrigin.Begin);

                    // 编码转换，解决中文乱码
                    responesInfo = Regex.Unescape(responesInfo);

                    //返回参数
                    info.resultdata = responesInfo;
                    //返回代码
                    info.statuscode = response.StatusCode.ToString();

                    await responseBody.CopyToAsync(originalBodyStream);
                    context.Response.Body = originalBodyStream;
                }

                var json = JsonConvert.SerializeObject(info);
                logger.LogInformation(json);

            }
            catch(Exception ex)
            {

            }
        }


        /// <summary>
        /// 根据文件类型获取数据编码类型
        /// </summary>
        /// <param name="contentType">文件类型</param>
        /// <returns></returns>
        private Encoding GetEncoding(string contentType)
        {
            var mediaType = contentType == null ? default(MediaType) : new MediaType(contentType);
            var encoding = mediaType.Encoding;
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return encoding;
        }

        /// <summary>
        /// 获取客户端Ip
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetRemoteIp(HttpContext context)
        {
            var remoteIp = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(remoteIp))
            {
                remoteIp = context.Connection.RemoteIpAddress.ToString();
                if (remoteIp == "::1")
                {
                    remoteIp = "127.0.0.1";
                }
                return remoteIp;
            }
            return remoteIp;
        }

        /// <summary>
        /// 返回请求信息
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="code">返回编码</param>
        /// <param name="info">返回信息</param>
        /// <returns></returns>
        private static Task HandleExceptionAsync(HttpContext context, int code, string info)
        {
            object operateStatus = new
            {
                code = code,
                info = info
            };
            var result = JsonConvert.SerializeObject(operateStatus);
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }


        public record RequestInParam()
        {
            public string remoteIp { get; set; }
            public string path { get; set; }
            public string requesttype { get; set; }
            public string scheme { get; set; }
            public DateTime createTime { get; set; }

            public string requestdata { get; set; }

            public double actiontime { get; set; }

            public string resultdata { get; set; }

            public string statuscode { get; set; }
        }
    }
}
