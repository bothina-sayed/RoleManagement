using FluentValidation;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using RoleManagement.Application.Abstractions;
using RoleManagement.Application.Utils;
using RoleManagement.Application.Validations;
using RoleManagement.Application.ViewModels;
using RoleManagement.Domain.Abstractions;
using RoleManagement.Domain.Models;
using RoleManagement.Domain.ViewModels;
using RoleManagement.Domain.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Application.Services
{
    internal class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<Role> _logger;
        private readonly IGenericRepository<Role> _roleRepo;
        private readonly IValidator<CreateRoleViewModel> _createRoleValidation;
        private readonly IValidator<UpdateRoleViewModel> _updateRoleValidation;

        public RoleService(IMapper mapper,
                           ILogger<Role> logger,
                           IGenericRepository<Role> roleRepo,
                           IValidator<CreateRoleViewModel> createRoleValidation,
                           IValidator<UpdateRoleViewModel> updateRoleValidation)
        {
            _mapper = mapper;
            _logger = logger;
            _roleRepo = roleRepo;
            _createRoleValidation = createRoleValidation;
            _updateRoleValidation = updateRoleValidation;
        }

        public async Task<ResponseModel<string>> Create(CreateRoleViewModel viewModel)
        {
            try
            {
                var validationResult = await _createRoleValidation.ValidateAsync(viewModel);

                if (!validationResult.IsValid)
                    return ResponseModel<string>
                    .Error(Helpers.ArrangeValidationErrors(validationResult.Errors));

                var role = _mapper.Map<Role>(viewModel);
                await _roleRepo.Add(role);
                await _roleRepo.Save();
                return ResponseModel<string>.Success();

            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return ResponseModel<string>.Error();
        }
        public async Task<ResponseModel<string>> Delete(int id)
        {
            try
            {
                var role = await _roleRepo.GetById(id);

                if (role == null)
                    return ResponseModel<string>.Error("not found");


                _roleRepo.Delete(role);

                await _roleRepo.Save();

                return ResponseModel<string>.Success();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
            }
            return ResponseModel<string>.Error();
        }
        public ResponseModel<List<RoleViewModel>> Get()
        {
            try
            {
                var result = _roleRepo.Get();

                var roles = _mapper.Map<List<RoleViewModel>>(result);

                return ResponseModel<List<RoleViewModel>>.Success(roles);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
            }
            return ResponseModel<List<RoleViewModel>>.Error();

        }
        public async Task<ResponseModel<RoleViewModel>> Update(UpdateRoleViewModel viewModel)
        {
            try
            {
                var validationResult = await _updateRoleValidation.ValidateAsync(viewModel);

                if (!validationResult.IsValid)
                    return ResponseModel<RoleViewModel>
                    .Error(Helpers.ArrangeValidationErrors(validationResult.Errors));

                var role = await _roleRepo.GetById(viewModel.Id);
                role!.Name = viewModel.Name;

                _roleRepo.Update(role);

                await _roleRepo.Save();

                return ResponseModel<RoleViewModel>.Success(_mapper.Map<RoleViewModel>(role));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.ToString());
            }
            return ResponseModel<RoleViewModel>.Error();
        }
    }
}
