using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.System.User
{
    public class LoginRequestValidater : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidater()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is riquied !");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is riquied !").MinimumLength(6)
                .WithMessage("Password is at least 6 characters");

        }
    }
}
