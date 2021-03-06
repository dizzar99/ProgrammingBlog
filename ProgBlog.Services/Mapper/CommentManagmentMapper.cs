﻿using AutoMapper;
using ProgBlog.DataAccess.Models;
using ProgBlog.Services.Models.CommentManagment;
using System;

namespace ProgBlog.Services.Mapper
{
    class CommentManagmentMapper : Profile
    {
        public CommentManagmentMapper()
        {
            this.CreateMap<CreateCommentRequest, DbComment>()
                .ForMember(d => d.AuthorId, opt => opt.MapFrom(c => c.UserId))
                .ForMember(d => d.CreatedDate, opt => opt.MapFrom(c => DateTime.Now));
            this.CreateMap<DbComment, Comment>();
            this.CreateMap<Comment, UpdateCommentRequest>();
            this.CreateMap<UpdateCommentRequest, DbComment>();
        }
    }
}
