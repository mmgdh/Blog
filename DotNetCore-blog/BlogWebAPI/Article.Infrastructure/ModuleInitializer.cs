using ArticleService.Domain.IRepository;
using ArticleService.Infrastructure.Repository;
using CommonInfrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleService.Infrastructure
{
    class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<IArticleRepository,ArticleRepository>();
            services.AddScoped<IArticleClassifyRepository,ArticleClassifyRepository>();
            services.AddScoped<IArticleTagRepository,ArticleTagRepository>();
        }
    }
}
