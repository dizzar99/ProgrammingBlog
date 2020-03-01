using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgBlog.Common;
using ProgBlog.DataAccess;
using ProgBlog.Services;
using System.Collections.Generic;

namespace ProgramminBlog.DependencyInstallers
{
    public static class InstallerExtensions
    {
        public static void InstallServices(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = new List<IDependencyInstaller>
            {
                new ServicesDependencyRegistrationModule(),
                new DataAccessDependencyRegistrationModule(),
                new AuthInstaller(),
                new WebApiInstaller()
            };

            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
