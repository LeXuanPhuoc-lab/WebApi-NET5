using FluentValidation;
using FPTManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Validation
{
    public class AccountValidator : AbstractValidator<AccountModel>
    {
        public AccountValidator()
        {
            // Username validation
            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty()
                .WithMessage("Username is required");
            // Password validation
            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
                .WithMessage("Password must 8 length, contains at least one uppercase letter" +
                ", one digit and one special character");
            RuleFor(x => x.StudentId)
                .NotNull();
        }
    }
}
