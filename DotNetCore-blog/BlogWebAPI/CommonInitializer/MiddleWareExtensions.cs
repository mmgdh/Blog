using CommonMiddleWare;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonInitializer
{
    public static class MiddleWareExtensions
    {
        /// <summary>
        /// 操作日志中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequetOperateMiddleware(this IApplicationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            return builder.UseMiddleware<MyRecordMiddleWare>();
        }
    }
}
