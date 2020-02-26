using AutoMapper;
using ProgBlog.DataAccess.Models;
using ProgBlog.Services.Models;
using ProgBlog.Services.Models.ArticleManagment;
using ProgBlog.Services.Models.CommentManagment;
using ProgBlog.Services.Models.UserManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgBlog.Services.Mapper
{
    class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<DbArticle, ArticleListItem>()
                .ForMember(d => d.AuthorId, opt => opt.MapFrom(a => a.CreatedUserId));

            this.CreateMap<DbArticle, ArticleDetails>()
                .ForMember(a => a.AuthorId, opt => opt.MapFrom(d => d.CreatedUserId));

            this.CreateMap<DbUser, UserListItem>();
            this.CreateMap<DbUser, UserDetails>()
                .ForMember(u => u.Articles, opt => opt.MapFrom(d => new List<ArticleListItem>()));


            this.CreateMap<CreateUserRequest, DbUser>();
            this.CreateMap<CreateArticleRequest, DbArticle>()
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(a => DateTime.Now))
                .ForMember(d => d.CreatedUserId, opt => opt.MapFrom(a => a.AuthorId));

            this.CreateMap<UserDetails, UpdateUserRequest>()
                .ForMember(upd => upd.Articles, opt => opt.MapFrom(u => u.Articles.Select(a => a.Id)));

            this.CreateMap<UpdateUserRequest, DbUser>()
                .ForMember(d => d.Articles, opt => opt.MapFrom(u => u.Articles));

            this.CreateMap<CreateCommentRequest, DbComment>()
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(c => DateTime.Now));
            this.CreateMap<DbComment, Comment>();
            this.CreateMap<Comment, UpdateCommentRequest>();
        }
    }
}
