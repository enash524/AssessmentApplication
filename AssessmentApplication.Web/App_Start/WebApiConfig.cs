using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace AssessmentApplication
{
    public static class WebApiConfig
    {
        #region Public Properties

        public static string UrlPrefix => "api";

        public static string UrlPrefixRelative => "~/api";

        #endregion Public Properties

        #region Public Methods

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            WebApiFilterConfig.RegisterGlobalFilters(config.Filters);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: UrlPrefix + "/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            JsonMediaTypeFormatter jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        #endregion Public Methods
    }
}