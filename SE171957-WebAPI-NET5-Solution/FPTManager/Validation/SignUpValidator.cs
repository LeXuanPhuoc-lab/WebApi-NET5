using FluentValidation;
using FluentValidation.Results;
using FPTManager.Models;
using FPTManager.Models.Response;
using FPTManager.Payloads.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FPTManager.Validation
{
    public class SignUpValidator : AbstractValidator<SignUpRequest>
    {
        //public string UserName { get; set; }
        //public string Password { get; set; }
        //public string FirstName { get; set; }
        //public string MidName { get; set; }
        //public string LastName { get; set; }
        //public string Email { get; set; }

        //The validation rules themselves should be defined in the validator class’s constructor.
        public SignUpValidator()
        {

            //To specify a validation rule for a particular property, call the RuleFor
            // Email validation
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Not Valid Email");
            // FullName validation <- RuleSet
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Matches("^[a-z]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase)
                .WithMessage("Invalid firstname");
            RuleFor(x => x.MidName)
                .NotEmpty()
                .Matches("^[a-z]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase)
            .WithMessage("Invalid midname");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .Matches("^[a-z]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase)
                .WithMessage("Invalid lastname");
            // Username validation
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Username is required");
            // Password validation
            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
                .WithMessage("Password must 8 length, contains at least one uppercase letter" +
                ", one digit and one special character");
        }
    }

    // Validator Extension
    public static class SignUpValidatorExtension
    {
        public static ValidationProblemDetails ToProblemDetails(this ValidationResult results) 
        {
            var error = new ValidationProblemDetails 
            {
                Status = 400
            };
            foreach(var validationFailure in results.Errors)
            {
                if (error.Errors.ContainsKey(validationFailure.PropertyName))
                {
                    error.Errors[validationFailure.PropertyName] =
                        error.Errors[validationFailure.PropertyName]
                            .Concat(new[] { validationFailure.ErrorMessage }).ToArray();
                    continue;
                }

                error.Errors.Add(new KeyValuePair<string, string[]>(
                    validationFailure.PropertyName,
                    new[] { validationFailure.ErrorMessage }));
            }

            return error;
        }
    }
}
