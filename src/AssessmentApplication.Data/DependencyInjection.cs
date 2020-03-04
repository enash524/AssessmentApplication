using System.Data;
using System.Data.SqlClient;
using AssessmentApplication.Data.Interfaces;
using AssessmentApplication.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssessmentApplication.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDbConnection>(_ => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ISalesOrderDetailRepository, SalesOrderDetailRepository>();

            return services;
        }
    }
}
