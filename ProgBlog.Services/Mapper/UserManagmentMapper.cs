using AutoMapper;
using ProgBlog.DataAccess.Models;
using ProgBlog.Services.Models.ArticleManagment;
using ProgBlog.Services.Models.UserManagment;
using System.Collections.Generic;

namespace ProgBlog.Services.Mapper
{
    class UserManagmentMapper : Profile
    {
        public UserManagmentMapper()
        {
            this.CreateMap<DbUser, UserListItem>();
            this.CreateMap<DbUser, UserDetails>()
                .ForMember(u => u.Articles, opt => opt.MapFrom(d => new List<Article>()));
            this.CreateMap<UpdateUserRequest, DbUser>();
        }
    }
}
