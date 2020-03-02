using AutoMapper;
using ProgBlog.DataAccess.Models;
using ProgBlog.Services.Models.ArticleCategoryManagment;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Mapper
{
    class CategoryManagmentMapper : Profile
    {
        public CategoryManagmentMapper()
        {
            this.CreateMap<DbArticleCategory, Category>();
            this.CreateMap<DbArticleCategory, CategoryDetails>();
            this.CreateMap<CreateCategoryRequest, DbArticleCategory>();
        }
    }
}
