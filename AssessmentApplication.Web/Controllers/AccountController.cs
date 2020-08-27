using System.Web.Mvc;
using System.Web.Security;
using AssessmentApplication.Models.Account;

namespace AssessmentApplication.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        #region Public Methods

        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
                return Redirect(FormsAuthentication.DefaultUrl);

            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("the model state is not valid", "invalid username and/or password");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(loginModel.Username, false);
                return Redirect(FormsAuthentication.DefaultUrl);
            }

            return View("Login", loginModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public RedirectResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return Redirect(FormsAuthentication.LoginUrl);
        }

        #endregion Public Methods
    }
}
