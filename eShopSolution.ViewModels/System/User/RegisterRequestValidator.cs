using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.System.User
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FisrtName).NotEmpty().WithMessage("Fisrt Name is required").MaximumLength(200)
                .WithMessage("Fisrt Name can not over 200 characters");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required").MaximumLength(200)
                .WithMessage("Fisrt Name can not over 200 characters");

            RuleFor(x => x.DOB).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Birthday cannot greater than 100 years !");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("Email format not match");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required !");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name id required !");
            RuleFor(x => x.Password).NotEmpty().WithMessage("PassWord is required !").MinimumLength(6).WithMessage("PassWord is at least 6 charaters");

            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Confirm password is not match");
                }
            });

        }
    }
}
