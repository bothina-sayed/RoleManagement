using RoleManagement.Application.ViewModels;
using RoleManagement.Domain.ViewModels;

namespace RoleManagement.Application.Abstractions
{
    public interface IRoleService
    {
        Task<ResponseModel<string>> Create(CreateRoleViewModel viewModel);
        Task<ResponseModel<string>> Delete(int id);
        ResponseModel<List<RoleViewModel>> Get();
        Task<ResponseModel<RoleViewModel>> Update(UpdateRoleViewModel viewModel);
    }
}