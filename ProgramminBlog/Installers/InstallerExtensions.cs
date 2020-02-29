﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramminBlog.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServicesFromAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(t =>
                typeof(IInstaller).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
