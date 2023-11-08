using FluentValidation;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleManagement.Application
{
    public static class Boostrap
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            var x = 0;
            return services;
        }
    }
}
