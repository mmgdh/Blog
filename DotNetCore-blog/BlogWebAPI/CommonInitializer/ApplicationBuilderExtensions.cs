using EventBus;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonInitializer
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDefault(this IApplicationBuilder app)
        {
            app.UseEventBus();
            app.UseCors();//启用Cors
            return app;
        }
    }
}
