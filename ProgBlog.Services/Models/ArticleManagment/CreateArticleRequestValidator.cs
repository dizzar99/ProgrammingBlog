using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.ArticleManagment
{
    public class CreateArticleRequestValidator : AbstractValidator<CreateArticleRequest>
    {
        public CreateArticleRequestValidator()
        {
            this.RuleFor(a => a.Title).NotEmpty();
            this.RuleFor(a => a.Content).MaximumLength(2000);
            this.RuleFor(a => a.AuthorId).NotEmpty();
        }
    }
}
