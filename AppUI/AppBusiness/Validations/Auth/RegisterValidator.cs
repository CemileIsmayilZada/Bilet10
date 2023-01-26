using AppBusiness.ViewModels.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBusiness.Validations.Auth
{
    public class RegisterValidator:AbstractValidator<RegisterTeamViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username)
                 .NotEmpty()
                 .NotNull()
                 .MaximumLength(50);
            RuleFor(x => x.Email)
                  .NotEmpty()
                  .NotNull()
                  .EmailAddress()
                  .MaximumLength(50);
            RuleFor(x => x.Fullname)
                  .NotEmpty()
                  .NotNull()
                  .MaximumLength(50);
            RuleFor(x => x.Password)
                  .NotEmpty()
                  .NotNull();
            RuleFor(x => x.ConfirmedPassword)
                  .NotEmpty()
                  .NotNull()
                  .Equal(x => x.Password).WithMessage("Confirmed Password and Password must be same");



        }
    }
}
