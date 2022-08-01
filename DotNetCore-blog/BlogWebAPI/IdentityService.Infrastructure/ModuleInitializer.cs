using CommonInfrastructure;
using IdentityService.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure
{
    class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<IdDomainService>();
            services.AddScoped<IIdRepository, IdRepository>();
        }
    }
}
