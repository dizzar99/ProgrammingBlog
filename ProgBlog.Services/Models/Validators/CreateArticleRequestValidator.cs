using FluentValidation;

namespace ProgBlog.Services.Models.ArticleManagment
{
    public class CreateArticleRequestValidator : AbstractValidator<CreateArticleRequest>
    {
        public CreateArticleRequestValidator()
        {
            this.RuleFor(a => a.Title).NotEmpty();
            this.RuleFor(a => a.Content).MaximumLength(2000);
        }
    }
}
