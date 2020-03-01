using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProgBlog.Common;
using ProgBlog.DataAccess.Implementations;
using ProgBlog.DataAccess.Interfaces;

namespace ProgBlog.DataAccess
{
    public class DataAccessDependencyRegistrationModule : IDependencyInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            services.AddScoped<ApplicationContext>();
        }
    }
}
