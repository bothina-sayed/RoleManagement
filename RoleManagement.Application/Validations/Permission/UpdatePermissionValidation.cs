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
    internal class UpdatePermissionValidation : AbstractValidator<UpdatePermissionViewModel>
    {
        private readonly IGenericRepository<Permission> _permissionRepo;
        public UpdatePermissionValidation( IGenericRepository<Permission> permissionRepo)
        {
            _permissionRepo = permissionRepo;

            RuleFor(x => x.Id)
                .NotEmpty().MustAsync(isRoleExist).WithMessage("Permission is Not exist");
        }
        private async Task<bool> isRoleExist(int id, CancellationToken cancellationToken)
            => await _permissionRepo.IsExist(x => x.Id == id);
    }
}
