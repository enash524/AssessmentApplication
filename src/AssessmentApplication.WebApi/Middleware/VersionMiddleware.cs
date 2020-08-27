using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AssessmentApplication.WebApi.Middleware
{
    public class VersionMiddleware
    {
        private static readonly Assembly _entryAssembly = Assembly.GetEntryAssembly();
        private static readonly string _version = FileVersionInfo.GetVersionInfo(_entryAssembly.Location).FileVersion;
        private readonly RequestDelegate _next;

        public VersionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync(_version);
        }
    }
}
