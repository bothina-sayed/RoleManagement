using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RoleManagement.Application.Abstractions;
using RoleManagement.Application.Services;
using RoleManagement.Application.Validations;
using RoleManagement.Application.Validations.Auth;
using RoleManagement.Domain.ViewModels;
using RoleManagement.Domain.ViewModels.Auth;


namespace RoleManagement.Application
{
    public static class Boostrap
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<IAuthService,AuthService>();
            #endregion

            #region Validation
            services.AddScoped<IValidator<LoginViewModel>,LoginValidation>();
            services.AddScoped<IValidator<RegisterViewModel>, RegisterValidation>();
            #endregion
            return services;
        }
    }
}
