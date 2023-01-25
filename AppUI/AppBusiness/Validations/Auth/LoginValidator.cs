using AppBusiness.ViewModels.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBusiness.Validations.Auth
{
    public class LoginValidator:AbstractValidator<LoginTeamViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UsernameOrEmail)
                 .NotEmpty()
                 .NotNull()
                 .MaximumLength(50);
            RuleFor(x => x.Password)
                  .NotEmpty()
                  .NotNull();

            

        }
    }
}
