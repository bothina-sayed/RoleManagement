using Microsoft.AspNetCore.Http;
using RoleManagement.Application.ViewModels;
using RoleManagement.Domain.ViewModels;
using RoleManagement.Domain.ViewModels.Auth;

namespace RoleManagement.Application.Abstractions
{
    public interface IAuthService
    {
        Task<ResponseModel<string>> Login(LoginViewModel viewModel, HttpContext httpContent);
        Task<ResponseModel<string>> Register(RegisterViewModel viewModel);
    }
}