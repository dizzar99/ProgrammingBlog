using FluentValidation;

namespace ProgBlog.Services.Models.UserManagment
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            this.RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email address is required.")
                .EmailAddress()
                .WithMessage("Incorrect email address");
            this.RuleFor(u => u.Login)
                .NotEmpty()
                .WithMessage("User login can't be empty.");
        }
    }
}
