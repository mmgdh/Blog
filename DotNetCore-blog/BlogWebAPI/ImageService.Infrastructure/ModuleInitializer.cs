using CommonInitializer;
using FileService.Domain;
using FileService.Infrastructure.StorageClients;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure
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
