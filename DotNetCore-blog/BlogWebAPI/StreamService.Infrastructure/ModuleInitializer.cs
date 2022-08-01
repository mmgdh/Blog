using CommonInfrastructure;
using Microsoft.Extensions.DependencyInjection;
using StreamService.Domain;
using StreamService.Infrastructure.StorageClients;

namespace StreamService.Infrastructure
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<IUploadItemRepository, UploadItemRepository>();
            services.AddScoped<IStorageClient, LocalStorage>();
        }
    }
}
