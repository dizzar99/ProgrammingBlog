using AutoMapper;
using ProgBlog.DataAccess.Models;
using ProgBlog.Services.Models.ArticleManagment;
using ProgBlog.Services.Models.UserManagment;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Mapper
{
    class UserManagmentMapper : Profile
    {
        public UserManagmentMapper()
        {
            this.CreateMap<DbUser, UserListItem>();
            this.CreateMap<DbUser, UserDetails>()
                .ForMember(u => u.Articles, opt => opt.MapFrom(d => new List<ArticleListItem>()));
            this.CreateMap<CreateUserRequest, DbUser>();
            this.CreateMap<UpdateUserRequest, DbUser>();
        }
    }
}
