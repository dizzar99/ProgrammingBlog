﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgBlog.Services.Implementations;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Mapper;

namespace ProgramminBlog.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(mapper => MappingConfiguration.Init());
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}