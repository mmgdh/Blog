using CommomInterface;
using Microsoft.Extensions.DependencyInjection;

namespace CommomJWT
{
    class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
