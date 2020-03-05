using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProgramminBlog.DependencyInstallers
{
    public interface IDependencyInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
