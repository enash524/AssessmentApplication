using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AssessmentApplication.Application;
using AssessmentApplication.Application.Mappers;
using AssessmentApplication.Application.Queries.Sales.SalesOrderDetail;
using AssessmentApplication.Data;
using AssessmentApplication.Data.Profiles;
using AssessmentApplication.WebApi.Extensions;
using AssessmentApplication.WebApi.Middleware;
using AssessmentApplication.WebApi.Profiles;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AssessmentApplication.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // TODO - ADD TO STATIC DependencyInjection CLASSES IF POSSIBLE!!!
            services
                .AddAutoMapper(
                    typeof(SalesOrderDetailVmProfile),
                    typeof(SalesOrderDetailProfile),
                    typeof(SalesOrderHeaderProfile))
                .AddLogging(config =>
                {
                    config
                        .AddConfiguration(Configuration.GetSection("Logging"))
                        .AddConsole(config =>
                        {
                            config.TimestampFormat = "yyyy-MM-dd hh:mm:ss tt ";
                        });
                })
                .AddApplication()
                .AddData(Configuration)
                .AddControllers(opt =>
                {
                    // remove formatter that turns nulls into 204 - No Content responses
                    // this formatter breaks Angular's Http response JSON parsing
                    opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetSalesOrderDetailQueryValidator>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services
                .AddHealthChecks()
                .AddMemoryHealthCheck("memory");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapVersion("/version");
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = WriteResponse
                });
                endpoints.MapControllers();
            });
        }

        private static Task WriteResponse(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            JsonWriterOptions options = new JsonWriterOptions
            {
                Indented = true
            };

            using (MemoryStream stream = new MemoryStream())
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(stream, options))
                {
                    writer.WriteStartObject();
                    writer.WriteString("status", result.Status.ToString());
                    writer.WriteStartObject("results");

                    foreach (KeyValuePair<string, HealthReportEntry> entry in result.Entries)
                    {
                        writer.WriteStartObject(entry.Key);
                        writer.WriteString("status", entry.Value.Status.ToString());
                        writer.WriteString("description", entry.Value.Description);
                        writer.WriteStartObject("data");

                        foreach (KeyValuePair<string, object> item in entry.Value.Data)
                        {
                            writer.WritePropertyName(item.Key);
                            JsonSerializer.Serialize(
                                writer, item.Value, item.Value?.GetType() ??
                                typeof(object));
                        }

                        writer.WriteEndObject();
                        writer.WriteEndObject();
                    }

                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }

                string json = Encoding.UTF8.GetString(stream.ToArray());

                return context.Response.WriteAsync(json);
            }
        }
    }
}
