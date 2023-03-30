using System;
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
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

string _policyName = "CorsPolicy";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json");

// Add services to the container.

builder.Services
    .AddAutoMapper(
        typeof(ApplicationMappingProfile),
        typeof(DataMappingProfile),
        typeof(ApiMappingProfile))
    .AddLogging(config =>
    {
        config.AddConfiguration(builder.Configuration.GetSection("Logging")).AddSimpleConsole(config =>
        {
            config.TimestampFormat = "yyyy-MM-dd hh:mm:ss tt ";
        });
    })

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc(
            "v1",
            new OpenApiInfo
            {
                Title = "",
                Version = "v1"
            });
        c.IncludeXmlComments(GetXmlPath());
    })
    .AddCors(options =>
    {
        options.AddPolicy(name: _policyName,
            builder =>
            {
                // TODO - NEED TO MAKE THIS DYNAMIC!!!
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
    })
    .AddApplication()
    .AddData(builder.Configuration)
    .AddControllers(opt =>
    {
        // remove formatter that turns nulls into 204 - No Content responses
        // this formatter breaks Angular's Http response JSON parsing
        opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
    });

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<GetSalesOrderDetailQueryValidator>();

builder.Services
    .AddHealthChecks()
    .AddMemoryHealthCheck("memory");

WebApplication app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}

app
    .UseHttpsRedirection()
    .UseRouting()
    .UseCors(_policyName)
    .UseAuthorization()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapVersion("/version");
        endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = WriteResponse
        });
    });

app
    .MapControllers()
    .RequireCors(_policyName);

app.Run();

static string GetXmlPath()
{
    string basePath = AppDomain.CurrentDomain.BaseDirectory;
    return Path.Combine(basePath, "AssessmentApplication.WebApi.xml");
}

static Task WriteResponse(HttpContext context, HealthReport result)
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
