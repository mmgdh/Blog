using CommomInterface;
using Microsoft.Extensions.DependencyInjection;

namespace ArticleService.Domain
{
    class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<ArticleDomainService>();
        }
    }
}
