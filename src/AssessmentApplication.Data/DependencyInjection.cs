using System.Data;
using System.Data.SqlClient;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssessmentApplication.Data
{
    /// <summary>
    /// Provides methods for adding AssessmentApplication.Data to the dependency injection service collection.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds dependency injection for the AssessmentApplication.Data project to the services collection.
        /// </summary>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add the AssessmentApplication.Data to.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <returns>The Microsoft.Extensions.DependencyInjection.IServiceCollection so that additional calls can be chained.</returns>
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDbConnection>(_ => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ISalesRepository, SalesRepository>();

            return services;
        }
    }
}
