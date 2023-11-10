using Microsoft.AspNetCore.Http;
using RoleManagement.Application.ViewModels;
using RoleManagement.Domain.ViewModels;
using RoleManagement.Domain.ViewModels.Auth;

namespace RoleManagement.Application.Abstractions
{
    public interface IAuthService
    {
        Task<ResponseModel<AuthViewModel>> Login(LoginViewModel viewModel);
        Task<ResponseModel<string>> Register(RegisterViewModel viewModel);
    }
}