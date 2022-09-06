using CommomInterface;
using Microsoft.Extensions.DependencyInjection;

namespace BlogInfoService.Domain
{
    internal class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<BlogParameterDomainService>();
        }
    }
}
