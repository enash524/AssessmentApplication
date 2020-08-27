﻿using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AssessmentApplication.Application
{
    /// <summary>
    /// Provides methods for adding AssessmentApplication.Application to the dependency injection service collection.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds dependency injection for the AssessmentApplication.Application project to the services collection.
        /// </summary>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add the AssessmentApplication.Application to.</param>
        /// <returns>The Microsoft.Extensions.DependencyInjection.IServiceCollection so that additional calls can be chained.</returns>

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
