using System.Web.Mvc;

namespace AssessmentApplication.Controllers
{
	public class HomeController : Controller
	{
		#region Public Methods

		public ViewResult Index()
		{
			return View("Index");
		}

		#endregion Public Methods
	}
}