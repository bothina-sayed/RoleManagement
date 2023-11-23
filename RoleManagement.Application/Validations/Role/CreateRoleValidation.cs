using FluentValidation;
using RoleManagement.Application.Utils;
using RoleManagement.Domain.Abstractions;
using RoleManagement.Domain.Models;
using RoleManagement.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Application.Validations
{
    internal class CreateRoleValidation : AbstractValidator<CreateRoleViewModel>
    {
        private readonly IGenericRepository<Role> _RoleRepo;
        public CreateRoleValidation(IGenericRepository<Role> roleRepo)
        {
            _RoleRepo = roleRepo;

            RuleFor(x => x.Name)
                .NotEmpty().MustAsync(isRoleExist).WithMessage("role is already exist");
        }
        private async Task<bool> isRoleExist(string role, CancellationToken cancellationToken)
            =>!await _RoleRepo.IsExist(x => x.Name == role);
           

    }
}
