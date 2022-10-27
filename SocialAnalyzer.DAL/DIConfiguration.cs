using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SocialAnalyzer.DAL.DataStores;
using SocialAnalyzer.DAL.Interfaces;
using SocialAnalyzer.DAL.Models;

namespace SocialAnalyzer.DAL
{
    public static class DIConfiguration
    {
        public static IServiceCollection ConfigureDALDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPlanDataStore, PlanDataStore>();
            services.AddScoped<IUsuarioDataStore, UsuarioDataStore>();
            services.AddScoped<ILoginUsuarioDataStore, LoginUsuarioDataStore>();


            return services;
        }

        public static void ConfigureDALContext(this IServiceCollection services, string dbConnection)
        {
            services.AddDbContext<analizerContext>(options =>
            {
                options.UseMySQL(dbConnection);
            });

        }

    }
}
