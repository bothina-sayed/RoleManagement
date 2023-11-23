using RoleManagement.Application.ViewModels;
using RoleManagement.Domain.ViewModels;

namespace RoleManagement.Application.Abstractions
{
    public interface IPermissionService
    {
        Task<ResponseModel<string>> Create(CreatePermissionViewModel viewModel);
        Task<ResponseModel<string>> Delete(int id);
        ResponseModel<List<PermissionViewModel>> Get();
        Task<ResponseModel<PermissionViewModel>> Update(UpdatePermissionViewModel viewModel);
    }
}