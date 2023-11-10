using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RoleManagement.Application.Abstractions;
using RoleManagement.Application.Specifications.Auth;
using RoleManagement.Application.Utils;
using RoleManagement.Application.ViewModels;
using RoleManagement.Domain.Abstractions;
using RoleManagement.Domain.Models;
using RoleManagement.Domain.ViewModels;
using RoleManagement.Domain.ViewModels.Auth;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Data;

namespace RoleManagement.Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<User> _logger;
        private readonly IGenericRepository<User> _UserRepo;
        private readonly IValidator<RegisterViewModel> _registerValidator;
        private readonly IValidator<LoginViewModel> _loginValidator;

        public AuthService(IMapper mapper, ILogger<User> logger, IGenericRepository<User> userRepo, IValidator<RegisterViewModel> registerValidator, IValidator<LoginViewModel> loginValidator)
        {
            _mapper = mapper;
            _logger = logger;
            _UserRepo = userRepo;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        public async Task<ResponseModel<string>> Register(RegisterViewModel viewModel)
        {
            try
            {
                var validationResult = await _registerValidator.ValidateAsync(viewModel);

                if (!validationResult.IsValid)
                    ResponseModel<string>
                        .Error(Helpers.ArrangeValidationErrors(validationResult.Errors));

                var key = Cipher.GenerateRandomKey();
                var password = Cipher.Encrypt(viewModel.Password, key);
                User user = new()
                {
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    Password = password,
                    EncryptionKey = key,
                };

                await _UserRepo.Add(user);
                await _UserRepo.Save();

                return ResponseModel<string>.Success();
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return ResponseModel<string>.Error();
        }
        public async Task<ResponseModel<AuthViewModel>> Login(LoginViewModel viewModel)
        {
            try
            {
                var validationResult = await _loginValidator.ValidateAsync(viewModel);

                if (!validationResult.IsValid)
                    return ResponseModel<AuthViewModel>
                        .Error(Helpers.ArrangeValidationErrors(validationResult.Errors));

                var user = _UserRepo.GetEntityWithSpec(new GetUserWithRoleSpecification(viewModel.Email));

                if (user.data == null)
                    return ResponseModel<AuthViewModel>.Error();

                var mappObject = _mapper.Map<AuthViewModel>(user.data);
                mappObject.Role = user.data.Role.Name;

                return ResponseModel<AuthViewModel>.Success(mappObject);
            }
            catch (Exception ex) { _logger.Log(LogLevel.Error, ex.ToString()); }

            return ResponseModel<AuthViewModel>.Error();
        }

    }
}
