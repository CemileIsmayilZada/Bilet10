using AppBusiness.ViewModels.TeamMemberrs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppBusiness.Validations.TeamMembers
{
    public class UpdateTeamMemberValidator : AbstractValidator<UpdateTeamMemberVm>
    {
        public UpdateTeamMemberValidator()
        {
            RuleFor(x => x.Id).Custom((Id, context) =>
            {
                if (!int.TryParse(Id.ToString(), out int id))
                {
                    context.AddFailure("enter valid format");
                }
            });
            RuleFor(x => x.Name)
                  .NotEmpty()
                  .NotNull()
                  .MaximumLength(50);
            RuleFor(x => x.Position)
                  .NotEmpty()
                  .NotNull()
                  .MaximumLength(50);
            RuleFor(x => x.Instagram)
                  .NotEmpty()
                  .NotNull()
                  .MaximumLength(50);
            RuleFor(x => x.Facebook)
                  .NotEmpty()
                  .NotNull()
                  .MaximumLength(50);
            RuleFor(x => x.Twitter)
                  .NotEmpty()
                  .NotNull()
                  .MaximumLength(50);
            
        }
    }
}
