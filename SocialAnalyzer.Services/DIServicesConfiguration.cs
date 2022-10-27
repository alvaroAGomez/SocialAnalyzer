using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SocialAnalyzer.DAL;
using SocialAnalyzer.Services.Helpers.JWT;
using SocialAnalyzer.Services.Interfaces;
using SocialAnalyzer.Services.Services;

namespace SocialAnalyzer.Services
{

    public static class DIServicesConfiguration
    {
        public static IServiceCollection ConfigureServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IJWTHelper, JWTHelper>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsuarioService, UsuarioService>();




            services.ConfigureDALDependencies();

            return services;
        }

        public static void ConfigureServiceDbContexts(this IServiceCollection services, string connectionString)
        {
            services.ConfigureDALContext(connectionString);
        }

        public static void ConfigureServiceMappingProfiles(this IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<Helpers.MapperProfiles>();
        }
    }
}
