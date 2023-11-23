using FluentValidation;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using RoleManagement.Application.Abstractions;
using RoleManagement.Application.Utils;
using RoleManagement.Application.ViewModels;
using RoleManagement.Domain.Abstractions;
using RoleManagement.Domain.Models;
using RoleManagement.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Application.Services
{
    internal class PermissionService : IPermissionService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<Permission> _logger;
        private readonly IGenericRepository<Permission> _permissionRepo;
        private readonly IValidator<CreatePermissionViewModel> _createPermissionValidation;
        private readonly IValidator<UpdatePermissionViewModel> _updatePermissionValidation;

        public PermissionService(IMapper mapper,
                                 ILogger<Permission> logger,
                                 IGenericRepository<Permission> permissionRepo,
                                 IValidator<CreatePermissionViewModel> createPermissionValidation,
                                 IValidator<UpdatePermissionViewModel> updatePermissionValidation)
        {
            _mapper = mapper;
            _logger = logger;
            _permissionRepo = permissionRepo;
            _createPermissionValidation = createPermissionValidation;
            _updatePermissionValidation = updatePermissionValidation;
        }

        public async Task<ResponseModel<string>> Create(CreatePermissionViewModel viewModel)
        {
            try
            {
                var validationResult = await _createPermissionValidation.ValidateAsync(viewModel);

                if (!validationResult.IsValid)
                    return ResponseModel<string>
                    .Error(Helpers.ArrangeValidationErrors(validationResult.Errors));

                var permission = _mapper.Map<Permission>(viewModel);
                await _permissionRepo.Add(permission);
                await _permissionRepo.Save();
                return ResponseModel<string>.Success();

            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return ResponseModel<string>.Error();
        }
        public async Task<ResponseModel<string>> Delete(int id)
        {
            try
            {
                var permission = await _permissionRepo.GetById(id);

                if (permission == null)
                    return ResponseModel<string>.Error("not found");


                _permissionRepo.Delete(permission);

                await _permissionRepo.Save();

                return ResponseModel<string>.Success();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
            }
            return ResponseModel<string>.Error();
        }
        public ResponseModel<List<PermissionViewModel>> Get()
        {
            try
            {
                var result = _permissionRepo.Get();

                var permssions = _mapper.Map<List<PermissionViewModel>>(result);

                return ResponseModel<List<PermissionViewModel>>.Success(permssions);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
            }
            return ResponseModel<List<PermissionViewModel>>.Error();

        }
        public async Task<ResponseModel<PermissionViewModel>> Update(UpdatePermissionViewModel viewModel)
        {
            try
            {
                var validationResult = await _updatePermissionValidation.ValidateAsync(viewModel);

                if (!validationResult.IsValid)
                    return ResponseModel<PermissionViewModel>
                    .Error(Helpers.ArrangeValidationErrors(validationResult.Errors));

                var permission = await _permissionRepo.GetById(viewModel.Id);
                permission!.Name = viewModel.Name;

                _permissionRepo.Update(permission);

                await _permissionRepo.Save();

                return ResponseModel<PermissionViewModel>.Success(_mapper.Map<PermissionViewModel>(permission));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
            }
            return ResponseModel<PermissionViewModel>.Error();
        }
    }

}
