using CommomFilters.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace CommomFilters
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public readonly ILogger<ExceptionFilter> logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            // 如果异常没有被处理则进行处理
            if (context.ExceptionHandled == false)
            {
                int code = 0;
                if (context.Exception is ServicesException)
                {
                    code = 1;
                }


                // 定义返回类型
                var result = new ApiResult
                {
                    Code = code,
                    Message = context.Exception.Message,
                    Data = ""
                };
                var JsonContent = JsonConvert.SerializeObject(result);
                context.Result = new ContentResult
                {
                    // 返回状态码设置为200，表示成功
                    StatusCode = (int)HttpStatusCode.OK,
                    // 设置返回格式
                    ContentType = "application/json;charset=utf-8",
                    Content = JsonContent
                };
                logger.LogError(JsonContent);
            }
            
            // 设置为true，表示异常已经被处理了
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }

        private class ApiResult
        {
            public int Code { get; set; }
            public string Message { get; set; }
            public string Data { get; set; }
        }
    }
}