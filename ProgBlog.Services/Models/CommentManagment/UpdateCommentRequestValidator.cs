using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.CommentManagment
{
    public class UpdateCommentRequestValidator : AbstractValidator<UpdateCommentRequest>
    {
        public UpdateCommentRequestValidator()
        {
            this.RuleFor(c => c.Text)
                .NotEmpty()
                .WithMessage("Comment should have some text.")
                .MaximumLength(200)
                .WithMessage("Comment text length should be less than 200 symbols.");
        }
    }
}
