using System.Web.Mvc;

namespace AssessmentApplication
{
	public class FilterConfig
	{
		#region Public Methods

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new AuthorizeAttribute());
		}

		#endregion Public Methods
	}
}