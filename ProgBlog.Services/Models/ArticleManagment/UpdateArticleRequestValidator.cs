using FluentValidation;

namespace ProgBlog.Services.Models.ArticleManagment
{
    public class UpdateArticleRequestValidator : AbstractValidator<UpdateArticleRequest>
    {
        public UpdateArticleRequestValidator()
        {
            this.RuleFor(a => a.Title).NotEmpty().WithMessage("Title length can't be 0.");
            this.RuleFor(a => a.Content).MaximumLength(2000);
        }
    }
}
