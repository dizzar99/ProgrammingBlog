using AutoMapper;

namespace ProgBlog.Services.Mapper
{
    public static class MappingConfiguration
    {
        public static IMapper Init()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<ArticleManagmentMapper>();
                mc.AddProfile<UserManagmentMapper>();
                mc.AddProfile<CommentManagmentMapper>();
                mc.AddProfile<UserCredentialsManagmentMapper>();
                mc.AddProfile<CategoryManagmentMapper>();
            });

            var mapper = mappingConfig.CreateMapper();
            return mapper;
        }
    }
}
