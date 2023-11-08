using Microsoft.Extensions.DependencyInjection;
using RoleManagement.Domain.Abstractions;
using RoleManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Infrastructure
{
    public static class Bootstrap
    {
        public static IServiceCollection InfrastractureStrapping(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // services.AddScoped<ITokenExtractor, TokenExtractor>();

            return services;
        }
    }
}
