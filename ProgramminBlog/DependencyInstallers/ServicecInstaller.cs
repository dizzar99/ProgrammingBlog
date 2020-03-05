using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgBlog.Services.Implementations;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Mapper;

namespace ProgramminBlog.DependencyInstallers
{
    public class ServicecInstaller : IDependencyInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(mapper => MappingConfiguration.Init());
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
        }
    }
}
