using FluentValidation;

namespace ProgBlog.Services.Models.CommentManagment
{
    public class CreateCommentRequestValidator : AbstractValidator<CreateCommentRequest>
    {
        public CreateCommentRequestValidator()
        {
            this.RuleFor(c => c.Text)
                .NotEmpty()
                .WithMessage("Comment should have some text.")
                .MaximumLength(200)
                .WithMessage("Comment text length should be less than 200 symbols.");
        }
    }
}
