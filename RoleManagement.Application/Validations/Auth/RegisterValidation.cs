using FluentValidation;
using RoleManagement.Domain.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Application.Validations
{
    internal class RegisterValidation : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Please enter your Name");

            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress().WithMessage("Please enter correct Email Format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Please enter your Password");
        }
    }
}
