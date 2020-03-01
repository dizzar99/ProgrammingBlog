using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProgBlog.Common
{
    public interface IDependencyInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
