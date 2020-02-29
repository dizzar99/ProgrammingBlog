using AutoMapper;
using ProgBlog.DataAccess.Models;
using ProgBlog.Services.Models.ArticleManagment;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Mapper
{
    class ArticleManagmentMapper : Profile
    {
        public ArticleManagmentMapper()
        {
            this.CreateMap<DbArticle, ArticleListItem>()
                .ForMember(d => d.AuthorId, opt => opt.MapFrom(a => a.CreatedUserId));
            this.CreateMap<DbArticle, ArticleDetails>()
                .ForMember(a => a.AuthorId, opt => opt.MapFrom(d => d.CreatedUserId));
            this.CreateMap<CreateArticleRequest, DbArticle>()
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(a => DateTime.Now))
                .ForMember(d => d.CreatedUserId, opt => opt.MapFrom(a => a.UserId));
            this.CreateMap<UpdateArticleRequest, DbArticle>();
        }
    }
}
