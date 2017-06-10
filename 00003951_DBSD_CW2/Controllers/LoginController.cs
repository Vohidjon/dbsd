using _00003951_DBSD_CW2.Models;
using System.Web.Mvc;
using System.Web.Security;
using _00003951_DBSD_CW2.DataAccess;

namespace _00003951_DBSD_CW2.Controllers
{
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        public ActionResult Index()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel, string returnUrl)
        {
            RecruiterManager manager = new RecruiterManager();
            bool authenticated = manager.Authenticate(loginModel.Email, loginModel.Password);
            if (authenticated)
            {
                FormsAuthentication.SetAuthCookie(loginModel.Email, false);

                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Vacancy");
                }
            }
            else
            {
                ModelState.AddModelError("", "Authentication failed!");
                return View("Index", loginModel);
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Recruiter recruiter)
        {
            try
            {
                RecruiterManager manager = new RecruiterManager();
                manager.CreateRecruiter(recruiter);
                FormsAuthentication.SetAuthCookie(recruiter.RecruiterEmail, false);

                return RedirectToAction("Index", "Vacancy");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

    }
}