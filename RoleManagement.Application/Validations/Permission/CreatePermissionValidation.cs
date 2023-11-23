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
    internal class CreatePermissionValidation : AbstractValidator<CreatePermissionViewModel>
    {
        private readonly IGenericRepository<Permission> _permissionRepo;
        public CreatePermissionValidation(IGenericRepository<Permission> permissionRepo)
        {
            _permissionRepo = permissionRepo;

            RuleFor(x => x.Name)
                .NotEmpty().MustAsync(isPermissionExist).WithMessage("Permission is already exist");
        }
        private async Task<bool> isPermissionExist(string permission, CancellationToken cancellationToken)
            => !await _permissionRepo.IsExist(x => x.Name == permission);
    }
}
