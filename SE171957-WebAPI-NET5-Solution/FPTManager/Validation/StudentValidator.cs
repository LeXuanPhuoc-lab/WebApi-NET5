using FluentValidation;
using FPTManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTManager.Validation
{
    public class StudentValidator : AbstractValidator<StudentModel>
    {
        //The validation rules themselves should be defined in the validator class’s constructor.
        public StudentValidator()
        {
            //public string FirstName { get; set; }
            //public string MidName { get; set; }
            //public string LastName { get; set; }

            //To specify a validation rule for a particular property, call the RuleFor
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Matches("^[a-zA-Z]+$")
                .WithMessage("First name not contain number or special character");
            RuleFor(x => x.MidName)
                .NotEmpty()
                .Matches("^[a-zA-Z]+$")
                .WithMessage("Mid name not contain number or special character");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .Matches("^[a-zA-Z]+$")
                .WithMessage("Last name not contain number or special character");
        }
    }
}
