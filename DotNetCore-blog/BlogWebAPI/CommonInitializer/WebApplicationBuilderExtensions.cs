using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CommonInfrastructure;
using Common.Commons;

namespace CommonInitializer
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigureDbConfiguration(this WebApplicationBuilder builder)
        {
            string connStr = builder.Configuration.GetValue<string>("ConnectionStrings:SqlServer");
            builder.Services.AddDbContext<BaseDbContext>(option =>
            {
                option.UseSqlServer(connStr);
            });
            //builder.Host.ConfigureAppConfiguration((hostCtx, configBuilder) =>
            //{
            //    //不能使用ConfigureAppConfiguration中的configBuilder去读取配置，否则就循环调用了，因此这里直接自己去读取配置文件
            //    var configRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            //    string connStr = configRoot.GetValue<string>("DefaultDB:ConnStr");
            //    string connStr = builder.Configuration.GetValue<string>("DefaultDB:ConnStr");
            //    builder.Services.AddDbContext<BaseDbContext>(option =>
            //    {
            //        option.UseSqlServer(connStr)
            //    });
            //});
        }
        public static void ConifgureExtraService(this WebApplicationBuilder builder)
        {
            IServiceCollection services = builder.Services;
            var assemblies = ReflectionHelper.GetAllReferencedAssemblies();
            services.RunModuleInitializers(assemblies);
        }
    }

}