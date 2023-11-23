using FluentValidation;
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
    internal class UpdateRoleValidation: AbstractValidator<UpdateRoleViewModel>
    {
        private readonly IGenericRepository<Role> _RoleRepo;
        public UpdateRoleValidation(IGenericRepository<Role> roleRepo)
        {
            _RoleRepo = roleRepo;

            RuleFor(x => x.Id)
                .NotEmpty().MustAsync(isRoleExist).WithMessage("role is Not exist");
        }
        private async Task<bool> isRoleExist(int id, CancellationToken cancellationToken)
            => await _RoleRepo.IsExist(x => x.Id == id);
    }
}
