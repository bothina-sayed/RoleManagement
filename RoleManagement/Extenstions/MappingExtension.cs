using Mapster;
using MapsterMapper;

namespace RoleManagement.Extenstions
{
    public static class MappingExtension
    {
        public static IServiceCollection AddMappingServices(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;

            services.AddSingleton(config);

            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
