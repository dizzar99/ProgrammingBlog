using FluentValidation;

namespace ProgBlog.Services.Models.IdentityManagment
{
    class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            this.RuleFor(r => r.Email).EmailAddress();
            this.RuleFor(r => r.Login).NotEmpty();
            this.RuleFor(r => r.Password).MinimumLength(4);
        }
    }
}
