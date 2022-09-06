using BlogInfoService.Domain.Entities.IRepository;
using BlogInfoService.Infrastructure.Repository;
using CommomInterface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogInfoService.Infrastructure
{
    internal class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<IBlogParamRepository, BlogParameterRepository>();
        }
    }
}
