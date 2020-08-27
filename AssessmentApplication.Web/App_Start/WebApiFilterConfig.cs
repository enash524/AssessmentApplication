using System.Web.Http;
using System.Web.Http.Filters;

namespace AssessmentApplication
{
    public class WebApiFilterConfig
    {
        #region Public Methods

        public static void RegisterGlobalFilters(HttpFilterCollection filters)
        {
            filters.Add(new AuthorizeAttribute());
        }

        #endregion Public Methods
    }
}
