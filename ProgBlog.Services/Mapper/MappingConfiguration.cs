using AutoMapper;
using System;
using System.Linq;

namespace ProgBlog.Services.Mapper
{
    public static class MappingConfiguration
    {
        public static IMapper Init()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                var profiles = typeof(MappingConfiguration).Assembly.GetTypes()
                    .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                    .Select(Activator.CreateInstance)
                    .Cast<Profile>()
                    .ToList();
                mc.AddProfiles(profiles);

                //mc.AddProfile<ArticleManagmentMapper>();
                //mc.AddProfile<UserManagmentMapper>();
                //mc.AddProfile<CommentManagmentMapper>();
                //mc.AddProfile<UserCredentialsManagmentMapper>();
                //mc.AddProfile<CategoryManagmentMapper>();
            });

            var mapper = mappingConfig.CreateMapper();
            return mapper;
        }
    }
}
