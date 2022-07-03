using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CommonInfrastructure;
using Common.Commons;
using FluentValidation.AspNetCore;

namespace CommonInitializer
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigureDbConfiguration<T>(this WebApplicationBuilder builder) where T : BaseDbContext
        {
            string connStr = builder.Configuration.GetValue<string>("ConnectionStrings:SqlServer");
            builder.Services.AddDbContext<T>(option =>
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

            services.AddCors(options =>
            {
                //更好的在Program.cs中用绑定方式读取配置的方法：https://github.com/dotnet/aspnetcore/issues/21491
                //不过比较麻烦。
                //var corsOpt = configuration.GetSection("Cors").Get<CorsSettings>();
                string[] urls = new[] { "http://localhost:3000", "http://localhost:83" };//corsOpt.Origins;
                options.AddDefaultPolicy(builder => builder.WithOrigins(urls)
                        .AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });
            services.AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblies(assemblies);
            });
        }
    }

}