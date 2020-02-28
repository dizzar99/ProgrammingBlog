using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

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
            });

            var mapper = mappingConfig.CreateMapper();
            return mapper;
        }
    }
}
