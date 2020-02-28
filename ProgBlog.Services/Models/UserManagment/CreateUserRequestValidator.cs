using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.UserManagment
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            this.RuleFor(u => u.Password)
                .MinimumLength(4)
                .WithMessage("Minimum password length is 4 symbols.");

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
